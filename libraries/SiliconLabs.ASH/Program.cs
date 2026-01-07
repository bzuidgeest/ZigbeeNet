using Microsoft.Extensions.Logging;
using SiliconLabs.ASH.V2;
using SiliconLabs.ASH.V3;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// Example program demonstrating ASH protocol v2/v3 with automatic version detection
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("ASH Protocol Example - Supports v2 (UG101) and v3 (UG115)");
            Console.WriteLine("=============================================================\n");
            
            // Setup logging
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Information);  // Debug level to see detailed messages
            });
            
            // Get available ports
            string[] ports = AshHost.GetAvailablePorts();
            if (ports.Length == 0)
            {
                Console.WriteLine("No serial ports found!");
                return;
            }
            
            Console.WriteLine($"Available ports: {string.Join(", ", ports)}\n");
            
            // Use first port or configure your port here
            string portName = ports[2];
            Console.WriteLine($"Using port: {portName}\n");

            // Create ASH host with explicit version configuration
            // Specify AshVersion.V2 for ASH v2 (UG101) or AshVersion.V3 for ASH v3 (UG115)
            using var ashHost = new AshHost(portName, 115200, loggerFactory, AshVersion.V2);
            
            // Wire up event handlers
            ashHost.IncomingFrameReceived += (sender, eventArgs) =>
            {
                Console.WriteLine($"?? Received payload: {AshHost.ToHexString(eventArgs.Frame.Data)}");
            };
            
            ashHost.StateChanged += (state) =>
            {
                Console.WriteLine($"?? State changed to: {state}");
            };
            
            ashHost.ErrorOccurred += (error) =>
            {
                Console.WriteLine($"? Error: {error}");
            };
            
            // Connect to NCP - version is auto-detected from RSTACK/RESET_ACK
            Console.WriteLine("Connecting to NCP...");
            bool connected = await ashHost.ConnectAsync(5000);
            
            if (!connected)
            {
                Console.WriteLine("\n? Failed to connect to NCP");
                return;
            }
            
            // Display detected version
            Console.WriteLine($"\n? Connected successfully!");
            Console.WriteLine($"?? Protocol version: {ashHost.Version.ToDisplayString()}\n");
            
            // Display version-specific features
            if (ashHost.Version == AshVersion.V2)
            {
                Console.WriteLine("v2 Features Active:");
                Console.WriteLine("  � DATA frame type for EZSP");
                Console.WriteLine("  � Data randomization (XOR)");
                Console.WriteLine("  � 2-byte CRC");
                Console.WriteLine("  � nRdy flow control");
                Console.WriteLine("  � ERROR frame support\n");
            }
            else if (ashHost.Version == AshVersion.V3)
            {
                Console.WriteLine("v3 Features Active:");
                Console.WriteLine("  � Data in ACK/NAK/RESET_ACK frames");
                Console.WriteLine("  � 3-byte expanded CRC");
                Console.WriteLine("  � 4-byte header with HEADER_ESCAPE\n");
            }
            
            // Listen for incoming data
            Console.WriteLine("\nListening for incoming data (press Ctrl+C to exit)...\n");
            
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
                Console.WriteLine("\nShutting down...");
            };
            
            try
            {
                await foreach (var ashFrame in ashHost.GetIncomingFrameStream(cts.Token))
                {
                    Console.WriteLine($"?? Received: {AshHost.ToHexString(ashFrame.Data)} ({ashFrame.Data.Length} bytes)");
                    
                    // Process payload here
                    // For example, decode EZSP frame:
                    // var ezspFrame = EzspFrame.Decode(payload);
                }
            }
            catch (OperationCanceledException)
            {
                // User pressed Ctrl+C
            }
            
            // Cleanup
            ashHost.Disconnect();
            Console.WriteLine("Disconnected.");
        }
    }
    
    /// <summary>
    /// Additional example methods
    /// </summary>
    public static class Examples
    {
        /// <summary>
        /// Example: Testing v2 codec directly
        /// </summary>
        public static void TestV2Codec()
        {
            Console.WriteLine("Testing ASH v2 Codec:");
            Console.WriteLine("=====================");
            
            var logger = LoggerFactory.Create(b => b.AddConsole())
                .CreateLogger<AshFrameCodecV2>();
            
            var codec = new AshFrameCodecV2(logger);
            
            // Create and encode DATA frame
            byte[] testData = [0x01, 0x02, 0x03];
            var frame = AshFrameV2.CreateDataFrame(frmNum: 1, ackNum: 1, testData);
            
            Console.WriteLine($"Original frame: {frame}");
            Console.WriteLine($"Original data: {BitConverter.ToString(testData)}");
            
            // Encode (includes data randomization for v2)
            byte[] encoded = codec.EncodeFrame(frame);
            Console.WriteLine($"Encoded (hex): {BitConverter.ToString(encoded)}");
            Console.WriteLine($"Encoded length: {encoded.Length} bytes");
            
            // Decode
            var decoded = codec.DecodeFrame(encoded) as AshFrameV2;
            if (decoded != null)
            {
                Console.WriteLine($"Decoded frame: {decoded}");
                Console.WriteLine($"Decoded data: {BitConverter.ToString(decoded.Data)}");
                Console.WriteLine($"Data matches: {CompareArrays(testData, decoded.Data)}");
            }
            
            Console.WriteLine();
        }
        
        /// <summary>
        /// Example: Testing v3 codec directly
        /// </summary>
        public static void TestV3Codec()
        {
            Console.WriteLine("Testing ASH v3 Codec:");
            Console.WriteLine("=====================");
            
            var logger = LoggerFactory.Create(b => b.AddConsole())
                .CreateLogger<AshFrameCodecV3>();
            
            var codec = new AshFrameCodecV3(logger);
            
            // Create and encode ACK frame with data (v3 carries data in ACK frames)
            byte[] testData = [0xAA, 0xBB, 0xCC];
            var frame = AshFrameV3.CreateAckFrame(ofc: 1, afc: 1, testData);
            
            Console.WriteLine($"Original frame: {frame}");
            Console.WriteLine($"Original data: {BitConverter.ToString(testData)}");
            
            // Encode (includes 3-byte expanded CRC for v3)
            byte[] encoded = codec.EncodeFrame(frame);
            Console.WriteLine($"Encoded (hex): {BitConverter.ToString(encoded)}");
            Console.WriteLine($"Encoded length: {encoded.Length} bytes");
            
            // Decode
            var decoded = codec.DecodeFrame(encoded) as AshFrameV3;
            if (decoded != null)
            {
                Console.WriteLine($"Decoded frame: {decoded}");
                Console.WriteLine($"Decoded data: {BitConverter.ToString(decoded.Data)}");
                Console.WriteLine($"Data matches: {CompareArrays(testData, decoded.Data)}");
            }
            
            Console.WriteLine();
        }
        
        private static bool CompareArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }
}
