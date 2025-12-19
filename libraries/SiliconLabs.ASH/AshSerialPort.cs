using System.IO.Ports;
using Microsoft.Extensions.Logging;
using SiliconLabs.ASH.V3;
using SiliconLabs.ASH.Common;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// Handles serial port communication for ASH protocol
    /// </summary>
    public class AshSerialPort : IDisposable
    {
        private readonly SerialPort _serialPort;
        private readonly ILogger<AshSerialPort> _logger;
        private readonly List<byte> _receiveBuffer = new List<byte>();
        private readonly object _bufferLock = new object();
        private bool _inFrame = false;
        private byte _headerEscape = 0;
        private readonly AshVersion _version;

        public event Action<byte[], byte>? OnFrameReceived; // frame data + header escape
        public event Action<string>? OnError;
        public event Action? OnWakeupReceived;

        /// <summary>
        /// Create ASH serial port handler
        /// </summary>
        public AshSerialPort(string portName, int baudRate, ILogger<AshSerialPort> logger, AshVersion version)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _version = version;

            _serialPort = new SerialPort(portName, baudRate)
            {
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                Handshake = Handshake.None,
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.ErrorReceived += SerialPort_ErrorReceived;
        }
        
        /// <summary>
        /// Open the serial port
        /// </summary>
        public void Open()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
                _logger.LogInformation("Serial port {PortName} opened at {BaudRate} baud", _serialPort.PortName, _serialPort.BaudRate);
                
                // Discard any buffered data
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();
            }
        }
        
        /// <summary>
        /// Close the serial port
        /// </summary>
        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _logger.LogInformation("Serial port {PortName} closed", _serialPort.PortName);
            }
        }
        
        /// <summary>
        /// Check if port is open
        /// </summary>
        public bool IsOpen => _serialPort.IsOpen;
        
        /// <summary>
        /// Send frame bytes
        /// </summary>
        public void SendFrame(byte[] frameBytes)
        {
            if (!_serialPort.IsOpen)
            {
                _logger.LogError("Serial port is not open");
                OnError?.Invoke("Serial port is not open");
                return;
            }
            
            try
            {
                _serialPort.Write(frameBytes, 0, frameBytes.Length);
                
                if (_logger.IsEnabled(LogLevel.Trace))
                {
                    _logger.LogTrace("Sent {ByteCount} bytes on {PortName}", frameBytes.Length, _serialPort.PortName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending frame on {PortName}", _serialPort.PortName);
                OnError?.Invoke("Error sending frame: " + ex.Message);
            }
        }
        
        /// <summary>
        /// Send wakeup byte(s)
        /// </summary>
        public void SendWakeup(int count = 1)
        {
            if (!_serialPort.IsOpen)
            {
                _logger.LogError("Serial port is not open");
                OnError?.Invoke("Serial port is not open");
                return;
            }
            
            try
            {
                byte[] wakeupBytes = new byte[count];
                Array.Fill(wakeupBytes, AshByteStuffing.WAKEUP);
                _serialPort.Write(wakeupBytes, 0, wakeupBytes.Length);
                
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Sent {Count} wakeup byte(s) on {PortName}", count, _serialPort.PortName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending wakeup on {PortName}", _serialPort.PortName);
                OnError?.Invoke("Error sending wakeup: " + ex.Message);
            }
        }
        
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = _serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = _serialPort.Read(buffer, 0, bytesToRead);

                if (bytesRead > 0)
                {
                    _logger.LogInformation("Received {ByteCount} bytes on {PortName}: {DataHex}",
                        bytesRead, _serialPort.PortName, BitConverter.ToString(buffer, 0, bytesRead).Replace("-", " "));
                }

                ProcessReceivedBytes(buffer, bytesRead);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error receiving data on {PortName}", _serialPort.PortName);
                OnError?.Invoke("Error receiving data: " + ex.Message);
            }
        }
        
        private void ProcessReceivedBytes(byte[] bytes, int length)
        {
            lock (_bufferLock)
            {
                for (int i = 0; i < length; i++)
                {
                    byte b = bytes[i];
                    
                    // Handle wakeup byte (0xFF) - only between frames
                    if (b == AshByteStuffing.WAKEUP && !_inFrame)
                    {
                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            _logger.LogDebug("Wakeup byte received");
                        }
                        
                        OnWakeupReceived?.Invoke();
                        continue;
                    }
                    
                    // Handle cancel byte
                    if (b == AshByteStuffing.CANCEL)
                    {
                        if (_inFrame)
                        {
                            if (_logger.IsEnabled(LogLevel.Debug))
                            {
                                _logger.LogDebug("Cancel byte received - discarding current frame");
                            }

                            _receiveBuffer.Clear();
                            _inFrame = false;
                            _headerEscape = 0;
                        }

                        // For V2, CANCEL precedes frames, so prepare to receive frame data
                        if (_version == AshVersion.V2)
                        {
                            _receiveBuffer.Clear();
                            _inFrame = true;
                            _headerEscape = 0;
                            if (_logger.IsEnabled(LogLevel.Debug))
                            {
                                _logger.LogDebug("CANCEL received - preparing for V2 frame");
                            }
                        }
                        continue;
                    }
                    
                    // Handle flag byte
                    if (b == AshByteStuffing.FLAG)
                    {
                        if (_version == AshVersion.V2)
                        {
                            // V2: FLAG ends the frame
                            if (_inFrame && _receiveBuffer.Count > 0)
                            {
                                _logger.LogInformation("FLAG delimiter received - processing V2 frame with {ByteCount} bytes", _receiveBuffer.Count);
                                ProcessCompleteFrame();
                            }

                            // Reset for next frame
                            _receiveBuffer.Clear();
                            _inFrame = false;
                            _headerEscape = 0;
                        }
                        else
                        {
                            // V3: FLAG starts the frame
                            if (_inFrame && _receiveBuffer.Count > 0)
                            {
                                // Next FLAG found - process previous frame
                                _logger.LogInformation("FLAG delimiter received - processing V3 frame with {ByteCount} bytes", _receiveBuffer.Count);
                                ProcessCompleteFrame();
                            }

                            // Start new frame
                            _receiveBuffer.Clear();
                            _inFrame = true;
                            _headerEscape = 0;

                            if (_logger.IsEnabled(LogLevel.Debug))
                            {
                                _logger.LogDebug("FLAG received - starting new V3 frame");
                            }
                        }
                    }
                    else if (_inFrame)
                    {
                        _receiveBuffer.Add(b);
                        _logger.LogDebug("Added byte to frame buffer: {Byte:X2}, buffer now has {Count} bytes", b, _receiveBuffer.Count);

                        // Check for buffer overflow
                        if (_receiveBuffer.Count > AshConstantsV3.MaxFrameLength)
                        {
                            _logger.LogWarning("Frame too long ({Length} bytes) - discarding", _receiveBuffer.Count);
                            OnError?.Invoke("Frame too long - discarding");
                            _receiveBuffer.Clear();
                            _inFrame = false;
                            _headerEscape = 0;
                            continue;
                        }
                        
                        // Check if we have complete frame (length-based detection)
                        if (_receiveBuffer.Count >= 3)
                        {
                            byte headerEscape = _receiveBuffer[0];
                            byte payloadLength = _receiveBuffer[2];
                            
                            // V3 header escape bit 0x04 indicates payload length is escaped
                            if ((headerEscape & 0x04) != 0)
                            {
                                payloadLength ^= AshByteStuffing.ESCAPE_XOR;
                            }
                            
                            int expectedMinLength = 1 + 1 + 1 + payloadLength + 3;
                            
                            if (_receiveBuffer.Count >= expectedMinLength)
                            {
                                TryProcessCompleteFrame();
                            }
                        }
                    }
                }
            }
        }
        
        private void TryProcessCompleteFrame()
        {
            try
            {
                if (_receiveBuffer.Count < 2)
                    return;
                
                byte headerEsc = _receiveBuffer[0];
                
                byte[] stuffedData = new byte[_receiveBuffer.Count - 1];
                _receiveBuffer.CopyTo(1, stuffedData, 0, stuffedData.Length);
                
                byte[] unstuffedData = AshByteStuffing.UnstuffBytes(stuffedData);
                
                if (unstuffedData.Length < 5)
                    return;
                
                byte control = unstuffedData[0];
                byte payloadLength = unstuffedData[1];
                
                // V3 header escape bits: 0x01 = control escaped, 0x04 = payload length escaped
                if ((headerEsc & 0x01) != 0)
                    control ^= AshByteStuffing.ESCAPE_XOR;
                if ((headerEsc & 0x04) != 0)
                    payloadLength ^= AshByteStuffing.ESCAPE_XOR;
                
                int expectedLength = 2 + payloadLength + 3;
                
                if (unstuffedData.Length >= expectedLength)
                {
                    _logger.LogInformation("Complete frame detected ({Length} bytes): {FrameHex}",
                        unstuffedData.Length, BitConverter.ToString(unstuffedData).Replace("-", " "));
                    OnFrameReceived?.Invoke(unstuffedData, headerEsc);
                    
                    _receiveBuffer.Clear();
                    _inFrame = false;
                    _headerEscape = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, "Frame processing attempt failed - waiting for more data");
            }
        }
        
        private void ProcessCompleteFrame()
        {
            if (_receiveBuffer.Count < 1)
            {
                _logger.LogWarning("Frame too short ({Length} bytes)", _receiveBuffer.Count);
                OnError?.Invoke("Frame too short");
                return;
            }

            byte[] unstuffedData;
            _headerEscape = 0;

            if (_version == AshVersion.V2)
            {
                // V2: No header escape byte, just unstuff the entire frame
                unstuffedData = AshByteStuffing.UnstuffBytes(_receiveBuffer.ToArray());
            }
            else
            {
                // V3: First byte is header escape, rest is stuffed data
                if (_receiveBuffer.Count < 2)
                {
                    _logger.LogWarning("V3 frame too short ({Length} bytes)", _receiveBuffer.Count);
                    OnError?.Invoke("Frame too short");
                    return;
                }

                _headerEscape = _receiveBuffer[0];

                byte[] stuffedData = new byte[_receiveBuffer.Count - 1];
                _receiveBuffer.CopyTo(1, stuffedData, 0, stuffedData.Length);

                unstuffedData = AshByteStuffing.UnstuffBytes(stuffedData);
            }

            if (unstuffedData.Length > 0)
            {
                _logger.LogInformation("Frame processed via FLAG delimiter ({Length} bytes): {FrameHex}",
                    unstuffedData.Length, BitConverter.ToString(unstuffedData).Replace("-", " "));
                OnFrameReceived?.Invoke(unstuffedData, _headerEscape);
            }
        }
        
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            _logger.LogError("Serial port error: {ErrorType}", e.EventType);
            OnError?.Invoke($"Serial port error: {e.EventType}");
        }
        
        /// <summary>
        /// Get available serial ports
        /// </summary>
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }
        
        public void Dispose()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _serialPort.Dispose();
        }
    }
}
