using Microsoft.Extensions.Logging;
using System.Threading.Channels;
using SiliconLabs.ASH.Common;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// High-level ASH protocol host interface
    /// </summary>
    public class AshHost : IDisposable
    {
        private readonly AshDataLink _dataLink;
        private readonly SemaphoreSlim _connectionSemaphore = new SemaphoreSlim(0, 1);
        private readonly ILogger<AshHost> _logger;
        
        // Channel for async stream consumption
        private readonly Channel<IAshFrame> _incomingFrameChannel;
        
        /// <summary>
        /// Fired when application payload data is received from the NCP.
        /// The byte array contains only the decoded application data (payload),
        /// with all ASH protocol overhead (frame headers, sequence numbers, CRC, byte stuffing) removed.
        /// This is NOT the raw frame data.
        /// </summary>
        //public event Action<byte[]>? PayloadReceived;
		public event EventHandler<AshFrameReceivedEventArgs>? IncomingFrameReceived;
        
        /// <summary>
        /// Fired when the ASH connection state changes.
        /// States: Disconnected, Connecting, Connected, Failed
        /// </summary>
        public event Action<AshState>? StateChanged;
        
        /// <summary>
        /// Fired when an error occurs during ASH protocol operations.
        /// Includes errors from serial communication, frame processing, timeouts, and protocol violations.
        /// </summary>
        public event Action<string>? ErrorOccurred;
        
        /// <summary>
        /// Fired when the payload channel is full and backpressure is active.
        /// This indicates that the consumer is too slow and payloads are being queued faster than processed.
        /// Applications should either speed up processing, increase channel capacity, or alert operators.
        /// The int parameter contains the current channel count (always equals capacity when this fires).
        /// </summary>
        public event Action<int>? ChannelFull;
        
        public bool IsConnected => _dataLink.State == AshState.Connected;
        public AshState State => _dataLink.State;
        
        /// <summary>
        /// Detected protocol version (available after connection)
        /// </summary>
        public AshVersion Version => _dataLink.Version;
        
        /// <summary>
        /// Create ASH host interface
        /// </summary>
        /// <param name="portName">Serial port name (e.g., "COM3")</param>
        /// <param name="baudRate">Baud rate (default: 115200)</param>
        /// <param name="loggerFactory">Logger factory for creating component loggers</param>
        /// <param name="version">ASH protocol version (V2 or V3). Must be explicitly specified.</param>
        /// <param name="payloadChannelCapacity">Maximum capacity of the payload channel (default: 100). Set to unbounded with -1.</param>
        public AshHost(string portName, int baudRate, ILoggerFactory loggerFactory, AshVersion version, int payloadChannelCapacity = 100)
        {
            ArgumentNullException.ThrowIfNull(loggerFactory);

            if (version == AshVersion.Unknown)
                throw new ArgumentException("ASH version must be explicitly specified (V2 or V3)", nameof(version));

            _logger = loggerFactory.CreateLogger<AshHost>();

            // Create bounded channel for payload streaming with backpressure support
            var channelOptions = new BoundedChannelOptions(payloadChannelCapacity > 0 ? payloadChannelCapacity : int.MaxValue)
            {
                FullMode = BoundedChannelFullMode.Wait, // Block sender if channel is full (backpressure)
                SingleReader = false, // Allow multiple consumers
                SingleWriter = true   // Only one writer (the ASH layer)
            };
            _incomingFrameChannel = Channel.CreateBounded<IAshFrame>(channelOptions);

            _dataLink = new AshDataLink(portName, baudRate, loggerFactory, version);
            
            // Wire up events
            _dataLink.OnDataReceived += (ashFrame) =>
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Data received: {ByteCount} bytes", ashFrame.Data.Length);
                }
                
                // Fire traditional event (for backward compatibility)
				IncomingFrameReceived?.Invoke(this, new AshFrameReceivedEventArgs(ashFrame));
                
                // Write to channel for async stream consumers (non-blocking with backpressure)
                if (!_incomingFrameChannel.Writer.TryWrite(ashFrame))
                {
                    // Channel is full - backpressure is active
                    int channelCount = _incomingFrameChannel.Reader.Count;
                    _logger.LogWarning("Payload channel full ({Count} items), backpressure active - consumer too slow", channelCount);
                    
                    // Notify application via event
                    ChannelFull?.Invoke(channelCount);
                }
            };
            
            _dataLink.OnStateChanged += HandleStateChanged;
            _dataLink.OnError += (error) =>
            {
                _logger.LogError("Error: {ErrorMessage}", error);
                ErrorOccurred?.Invoke(error);
            };
        }
        
        /// <summary>
        /// Connect to NCP
        /// </summary>
        /// <param name="timeoutMs">Connection timeout in milliseconds</param>
        /// <returns>True if connected successfully</returns>
        public async Task<bool> ConnectAsync(int timeoutMs = 5000)
        {
            try
            {
                _logger.LogInformation("Opening serial port and connecting to NCP (timeout: {TimeoutMs}ms)", timeoutMs);
                
                // Open serial port
                _dataLink.Open();
                
                // Initiate connection
                _dataLink.Connect();
                
                // Wait for connection with timeout
                using (var cts = new CancellationTokenSource(timeoutMs))
                {
                    try
                    {
                        await _connectionSemaphore.WaitAsync(cts.Token);
                        bool connected = _dataLink.State == AshState.Connected;
                        
                        if (connected)
                        {
                            _logger.LogInformation("Successfully connected to NCP");
                        }
                        else
                        {
                            _logger.LogWarning("Connection completed but state is {State}", _dataLink.State);
                        }
                        
                        return connected;
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogError("Connection timeout after {TimeoutMs}ms", timeoutMs);
                        ErrorOccurred?.Invoke("Connection timeout");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection failed");
                ErrorOccurred?.Invoke($"Connection failed: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Connect to NCP (synchronous)
        /// </summary>
        public bool Connect(int timeoutMs = 5000)
        {
            return ConnectAsync(timeoutMs).GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Disconnect from NCP
        /// </summary>
        public void Disconnect()
        {
            _logger.LogInformation("Disconnecting from NCP");
            _dataLink.Disconnect();
            _dataLink.Close();
        }
        
        /// <summary>
        /// Send data to NCP
        /// </summary>
        /// <param name="data">Data to send</param>
        /// <returns>True if data was queued for sending</returns>
        public bool SendData(byte[] data)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Sending {ByteCount} bytes", data.Length);
            }
            
            return _dataLink.SendData(data);
        }
        
        /// <summary>
        /// Send data to NCP (convenience method for strings)
        /// </summary>
        public bool SendData(string hexString)
        {
            try
            {
                byte[] data = ConvertHexStringToBytes(hexString);
                return SendData(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid hex string: {HexString}", hexString);
                ErrorOccurred?.Invoke($"Invalid hex string: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Get list of available serial ports
        /// </summary>
        public static String[] GetAvailablePorts()
        {
            return AshSerialPort.GetAvailablePorts();
        }
        
        /// <summary>
        /// Get an asynchronous stream of payload data received from the NCP.
        /// This provides a continuous stream of payloads that can be consumed with 'await foreach'.
        /// The stream will complete when the connection is closed or disposed.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the stream consumption</param>
        /// <returns>Async enumerable of payload byte arrays</returns>
        /// <example>
        /// <code>
        /// await foreach (var payload in ashHost.GetPayloadStream(cancellationToken))
        /// {
        ///     // Process payload continuously
        ///     ProcessEmberMessage(payload);
        /// }
        /// </code>
        /// </example>
        public async IAsyncEnumerable<IAshFrame> GetIncomingFrameStream([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var payload in _incomingFrameChannel.Reader.ReadAllAsync(cancellationToken))
            {
                yield return payload;
            }
        }
        
        /// <summary>
        /// Try to read a single payload from the stream asynchronously.
        /// Returns immediately if no data is available.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the read operation</param>
        /// <returns>Payload data if available, null if channel is completed</returns>
        public async ValueTask<IAshFrame?> TryReadIncomingFrameAsync(CancellationToken cancellationToken = default)
        {
            if (await _incomingFrameChannel.Reader.WaitToReadAsync(cancellationToken))
            {
                if (_incomingFrameChannel.Reader.TryRead(out var payload))
                {
                    return payload;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Wait for and read the next payload from the stream asynchronously.
        /// Blocks until data is available or the channel is completed.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the read operation</param>
        /// <returns>Payload data, or null if channel is completed</returns>
        public async ValueTask<IAshFrame?> ReadIncomingFrameAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _incomingFrameChannel.Reader.ReadAsync(cancellationToken);
            }
            catch (ChannelClosedException)
            {
                return null;
            }
        }
        
        private void HandleStateChanged(AshState newState)
        {
            _logger.LogInformation("State changed to {State}", newState);
            StateChanged?.Invoke(newState);
            
            // Signal connection semaphore when connected
            if (newState == AshState.Connected)
            {
                try
                {
                    _connectionSemaphore.Release();
                }
                catch (SemaphoreFullException)
                {
                    // Already signaled
                }
            }
        }
        
        /// <summary>
        /// Convert hex string to byte array
        /// </summary>
        private static byte[] ConvertHexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("-", "").Replace("0x", "");
            
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string must have even length");
            
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
        
        /// <summary>
        /// Convert byte array to hex string
        /// </summary>
        public static string ToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }
        
        public void Dispose()
        {
            // Complete the channel to signal end of stream (use TryComplete to avoid exception if already completed)
            _incomingFrameChannel.Writer.TryComplete();

            _dataLink.Dispose();
            _connectionSemaphore.Dispose();
        }
    }
}
