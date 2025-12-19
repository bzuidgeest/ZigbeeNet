using Microsoft.Extensions.Logging;
using SiliconLabs.ASH.Common;
using System;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// ASH protocol data link layer - integrates state machine and serial communication
    /// Requires explicit version configuration (V2 or V3)
    /// </summary>
    public class AshDataLink : IDisposable
    {
        private readonly AshSerialPort _serialPort;
        private IAshStateMachine? _stateMachine;
        private IAshFrameCodec? _codec;
        
        private readonly System.Threading.Timer _timer;
        private readonly object _sendLock = new object();
        private readonly ILogger<AshDataLink> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AshVersion _configuredVersion;

        private AshVersion _detectedVersion = AshVersion.Unknown;
        
        public event Action<byte[]>? OnDataReceived;
        public event Action<AshState>? OnStateChanged;
        public event Action<string>? OnError;
        
        public AshState State => _stateMachine?.State ?? AshState.Disconnected;
        
        /// <summary>
        /// Detected protocol version (available after connection)
        /// </summary>
        public AshVersion Version => _detectedVersion;
        
        /// <summary>
        /// Create ASH data link layer with explicit version configuration
        /// </summary>
        /// <param name="portName">Serial port name (e.g., "COM3")</param>
        /// <param name="baudRate">Baud rate (default: 115200)</param>
        /// <param name="loggerFactory">Logger factory for creating component loggers</param>
        /// <param name="version">ASH protocol version (V2 or V3). Must be explicitly specified.</param>
        public AshDataLink(string portName, int baudRate, ILoggerFactory loggerFactory, AshVersion version)
        {
            if (version == AshVersion.Unknown)
                throw new ArgumentException("ASH version must be explicitly specified (V2 or V3)", nameof(version));

            _configuredVersion = version;
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _logger = _loggerFactory.CreateLogger<AshDataLink>();

            _serialPort = new AshSerialPort(portName, baudRate, _loggerFactory.CreateLogger<AshSerialPort>(), version);
            
            // Wire up serial port events
            _serialPort.OnFrameReceived += HandleReceivedFrameBytes;
            _serialPort.OnError += (error) => OnError?.Invoke("Serial: " + error);
            _serialPort.OnWakeupReceived += () => 
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Wakeup byte received");
                }
            };
            
            // Timer for retransmissions (check every 100ms)
            _timer = new System.Threading.Timer(TimerCallback, null, 100, 100);
        }
        
        /// <summary>
        /// Open serial port and initialize connection
        /// </summary>
        public void Open()
        {
            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to open serial port");
                throw;
            }
        }
        
        /// <summary>
        /// Connect to NCP using ASH protocol with configured version
        /// </summary>
        public void Connect()
        {
            if (!_serialPort.IsOpen)
            {
                _logger.LogError("Serial port is not open");
                OnError?.Invoke("Serial port is not open");
                return;
            }

            // Initialize codec and state machine with configured version
            _detectedVersion = _configuredVersion;

            _logger.LogInformation("Initiating ASH connection using {Version}", _configuredVersion.ToDisplayString());

            // Create appropriate codec and state machine based on configured version
            _codec = AshFrameCodecFactory.Create(_configuredVersion, _logger);
            _stateMachine = AshStateMachineFactory.Create(_configuredVersion, _loggerFactory);

            // Wire up state machine events
            _stateMachine.OnDataReceived += (data) => OnDataReceived?.Invoke(data);
            _stateMachine.OnStateChanged += (state) => OnStateChanged?.Invoke(state);
            _stateMachine.OnError += (error) => OnError?.Invoke("State machine: " + error);
            _stateMachine.OnFrameToSend += SendFrame;

            // Initiate connection through state machine (this will send RST frame)
            _stateMachine.Connect();
        }
        
        /// <summary>
        /// Send wakeup to NCP
        /// </summary>
        public void SendWakeup(int count = 1)
        {
            _serialPort.SendWakeup(count);
        }
        
        /// <summary>
        /// Disconnect from NCP
        /// </summary>
        public void Disconnect()
        {
            _logger.LogInformation("Disconnecting from NCP");
            _stateMachine?.Disconnect();
            _detectedVersion = AshVersion.Unknown;
        }
        
        /// <summary>
        /// Close serial port
        /// </summary>
        public void Close()
        {
            _stateMachine?.Disconnect();
            _serialPort.Close();
        }
        
        /// <summary>
        /// Send data to NCP
        /// </summary>
        public bool SendData(byte[] data)
        {
            if (_stateMachine == null)
            {
                _logger.LogWarning("Cannot send data - not connected");
                OnError?.Invoke("Not connected");
                return false;
            }
            
            if (State != AshState.Connected)
            {
                _logger.LogWarning("Cannot send data - state is {State}", State);
                OnError?.Invoke("Cannot send data - state is " + State);
                return false;
            }
            
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Sending {ByteCount} bytes", data.Length);
            }
            
            return _stateMachine.SendData(data);
        }
        
        private void HandleReceivedFrameBytes(byte[] frameBytes, byte headerEscape)
        {
            try
            {
                // Check if codec is initialized
                if (_codec == null)
                {
                    _logger.LogWarning("Received frame before codec initialized: {FrameHex}",
                        BitConverter.ToString(frameBytes).Replace("-", " "));
                    return;
                }

                // Decode frame using configured codec
                IAshFrame? frame = _codec.DecodeFrame(frameBytes);

                if (frame == null)
                {
                    _logger.LogWarning("Failed to decode frame - CRC or format error");
                    OnError?.Invoke("Failed to decode frame - sending N-AK");

                    if (State == AshState.Connected)
                    {
                        _stateMachine?.SendEmptyAck();
                    }
                    return;
                }

                // Log only if Debug level is enabled
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug(
                        "Received {FrameType} [OFC={OFC}, AFC={AFC}] PayloadLength={PayloadLength}",
                        frame.FrameType,
                        frame.OutgoingFrameCounter,
                        frame.AckNackFrameCounter,
                        frame.Data.Length);
                }

                // Process frame in state machine
                _stateMachine?.ProcessReceivedFrame(frame);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing received frame");
                OnError?.Invoke("Error processing received frame: " + ex.Message);
            }
        }
        

        private void SendFrame(IAshFrame frame)
        {
            if (_codec == null)
            {
                _logger.LogError("Cannot send frame - codec not initialized");
                return;
            }
            
            byte[] encodedFrame;
            
            try
            {
                // Encode frame OUTSIDE of lock (CPU-bound work)
                encodedFrame = _codec.EncodeFrame(frame);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encoding frame");
                OnError?.Invoke("Error encoding frame: " + ex.Message);
                return;
            }
            
            // Only lock for serial port write operation
            lock (_sendLock)
            {
                try
                {
                    _serialPort.SendFrame(encodedFrame);
                    
                    // Log only if Debug level is enabled
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug(
                            "Sent {FrameType} [OFC={OFC}, AFC={AFC}] PayloadLength={PayloadLength}",
                            frame.FrameType,
                            frame.OutgoingFrameCounter,
                            frame.AckNackFrameCounter,
                            frame.Data.Length);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending frame");
                    OnError?.Invoke("Error sending frame: " + ex.Message);
                }
            }
        }
        
        private void TimerCallback(object? state)
        {
            try
            {
                _stateMachine?.ProcessTimers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Timer error");
                OnError?.Invoke("Timer error: " + ex.Message);
            }
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
            _serialPort?.Dispose();
        }
    }
}
