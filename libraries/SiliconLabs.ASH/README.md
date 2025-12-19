# ASH Protocol Implementation

This is a C# implementation of the ASH (Asynchronous Serial Host) protocol for communicating with Silicon Labs EmberZNet Serial Protocol (EZSP) Network Co-Processors (NCPs).

## Features

**? Dual Version Support:**
- **ASH v2 (UG101)** - Automatic detection and support
  - DATA frame type for EZSP
  - Data randomization (XOR with pseudo-random sequence)
  - 2-byte standard CRC-CCITT
  - nRdy flow control flag
  - ERROR frame support
- **ASH v3 (UG115)** - Full implementation (current default)
  - Data carried in ACK/NAK/RESET_ACK frames
  - 3-byte expanded CRC (no escaping needed)
  - 4-byte header with HEADER_ESCAPE
- **Automatic Version Detection** - Detects v2 or v3 from RSTACK/RESET_ACK frame

**?? Protocol Features:**
- CRC16-CCITT error detection with 3-byte expansion (ensures no escaping needed)
- 4-byte header: [FLAG] [HEADER_ESCAPE] [CONTROL] [PAYLOAD_LENGTH]
- Byte stuffing for reserved characters (0x7E, 0x7D, 0x11, 0x13, 0x18, 0x1A, 0xF8)
- Sliding window protocol (window size = 2, up to 2 frames in flight)
- Automatic retransmission on timeout or NACK
- Configurable retry count (default: 3)
- Retransmission timeout: 500ms (per spec)
- Frame counters: 1-7 range (3-bit counters, per spec)
- Maximum payload size: 57 bytes

? **Connection Management:**
- Explicit version configuration (V2 or V3) - no auto-detection
- Connection initialization with RESET/RESET_ACK handshake
- State machine: Disconnected ? Connecting ? Connected
- Connection timeout handling
- Async/sync connection methods
- Wakeup byte support (0xFF)

## Usage

### Basic Example

```csharp
using SiliconLabs.ASH.AshProtocol;
using Microsoft.Extensions.Logging;

// Setup logging
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Information);
});

// Create ASH host with explicit version configuration
// Specify AshVersion.V2 for ASH v2 (UG101) or AshVersion.V3 for ASH v3 (UG115)
using var ashHost = new AshHost("COM3", 115200, loggerFactory, AshVersion.V3);

// Wire up event handlers
ashHost.PayloadReceived += (data) =>
{
    Console.WriteLine($"Received payload: {AshHost.ToHexString(data)}");
};

ashHost.ErrorOccurred += (error) =>
{
    Console.WriteLine($"Error: {error}");
};

ashHost.ChannelFull += (count) =>
{
    Console.WriteLine($"Warning: Channel full ({count} items) - consumer too slow!");
};

// Connect to NCP
if (await ashHost.ConnectAsync(5000))
{
    Console.WriteLine("Connected!");

    // Check configured version
    Console.WriteLine($"Using {ashHost.Version.ToDisplayString()}");
    // Output: "ASH v2 (UG101)" or "ASH v3 (UG115)"

    // Send data
    byte[] data = { 0x01, 0x02, 0x03 };
    ashHost.SendData(data);

    // Or send from hex string
    ashHost.SendData("01 02 03");
}

// Disconnect
ashHost.Disconnect();
```

### Asynchronous Stream Processing (Recommended)

For continuous processing of payloads, use the async stream API instead of events:

```csharp
// Simple and clean!
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole().SetMinimumLevel(LogLevel.Information);
});

using var ashHost = new AshHost("COM3", 115200, loggerFactory, AshVersion.V3);

await ashHost.ConnectAsync(5000);

// Continuously process payloads as they arrive
await foreach (var payload in ashHost.GetPayloadStream(cancellationToken))
{
    Console.WriteLine($"Received: {AshHost.ToHexString(payload)}");

    // Process Ember/EZSP messages
    ProcessEmberMessage(payload);
}

ashHost.Disconnect();
```

**Benefits of async streams:**
- Non-blocking consumption
- Built-in backpressure handling
- Cancellation support
- Multiple concurrent consumers
- Perfect for continuous byte streams (Ember protocol parsing)

**Example:** See `Program.cs` for a complete working example with version detection and async stream processing.

### Logging Configuration

The library uses `ILoggerFactory` for dependency injection, automatically creating properly categorized loggers for all components:

- `SiliconLabs.ASH.AshProtocol.AshHost` - Main host
- `SiliconLabs.ASH.AshProtocol.AshDataLink` - Data link layer
- `SiliconLabs.ASH.AshProtocol.AshSerialPort` - Serial communication
- `SiliconLabs.ASH.AshProtocol.V2.AshStateMachineV2` - v2 state machine (when v2 detected)
- `SiliconLabs.ASH.AshProtocol.V3.AshStateMachineV3` - v3 state machine (when v3 detected)

**Quick Start:**
```csharp
// Development (verbose)
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Debug);
});

// Production (errors only)
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Warning);
});

// Version-specific filtering
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Information)
        .AddFilter("SiliconLabs.ASH.AshProtocol.V2", LogLevel.Debug)   // More verbose for v2
        .AddFilter("SiliconLabs.ASH.AshProtocol.V3", LogLevel.Warning); // Less verbose for v3
});
```

### Finding Available Ports

```csharp
string[] ports = AshHost.GetAvailablePorts();
foreach (string port in ports)
{
    Console.WriteLine(port);
}
```

### Events

The `AshHost` class provides the following events:

- **PayloadReceived** - Fired when application payload data is received from NCP. The byte array contains only the decoded application data with all ASH protocol overhead removed (no frame headers, sequence numbers, CRC, or byte stuffing).
- **StateChanged** - Fired when connection state changes (Disconnected, Connecting, Connected, Failed)
- **ErrorOccurred** - Fired on errors from serial communication, frame processing, timeouts, and protocol violations
- **ChannelFull** - Fired when the payload channel is full and backpressure is active. Indicates the consumer is too slow and payloads are being queued faster than processed. The event parameter contains the current channel count. Applications should speed up processing, increase channel capacity, or alert operators.

**Note:** For modern async applications, consider using `GetPayloadStream()` instead of the `PayloadReceived` event. The async stream API provides better backpressure handling and integrates seamlessly with async/await patterns.

### Async Stream Methods

The `AshHost` class provides the following async methods for stream-based payload consumption:

- **GetPayloadStream(CancellationToken)** - Returns an `IAsyncEnumerable<byte[]>` for continuous payload streaming. Use with `await foreach` for non-blocking consumption.
- **ReadPayloadAsync(CancellationToken)** - Asynchronously waits for and reads the next payload. Blocks until data is available or the stream completes.
- **TryReadPayloadAsync(CancellationToken)** - Attempts to read a payload without blocking. Returns `null` if no data is immediately available.


## Protocol Details

### ASH v2 vs v3 Comparison

| Feature | v2 (UG101) | v3 (UG115) |
|---------|------------|------------|
| **Data Transport** | DATA frame type | ACK/NAK/RESET_ACK with payload |
| **CRC** | 2-byte standard | 3-byte expanded (no escaping) |
| **Header** | 2-byte | 4-byte with HEADER_ESCAPE |
| **Data Randomization** | Yes (XOR sequence) | No |
| **Flow Control** | nRdy flag | Not specified |
| **ERROR Frame** | Yes | No |
| **Frame Counters** | 1-7 | 1-7 |
| **Max Payload** | 128 bytes | 57 bytes |

### Frame Format (v3)

```
[FLAG] [HEADER_ESCAPE] [CONTROL] [PAYLOAD_LENGTH] [DATA...] [CRC0] [CRC1] [CRC2]
```

- **FLAG**: 0x7E - marks frame start
- **HEADER_ESCAPE**: Bits indicate if CONTROL or PAYLOAD_LENGTH are escaped
  - Bit 7 (0x80): Set if CONTROL byte is escaped
  - Bit 6 (0x40): Set if PAYLOAD_LENGTH byte is escaped
- **CONTROL**: Frame type, OFC (Outgoing Frame Counter), and AFC (ACK/NACK Frame Counter)
  - Bits 7-6: Type (0=RESET, 1=RESET_ACK, 2=ACK, 3=NACK)
  - Bits 5-3: OFC (Outgoing Frame Counter, 1-7)
  - Bits 2-0: AFC (ACK/NACK Frame Counter, 1-7)
- **PAYLOAD_LENGTH**: Length of data payload (0-57 bytes)
- **DATA**: Application data (optional)
- **CRC**: 3-byte expanded CRC-16 (bit 4 cleared in all bytes to avoid escaping)

### Byte Stuffing

Reserved bytes that are escaped:
- 0x7E (FLAG) ? 0x7D 0x5E
- 0x7D (ESCAPE) ? 0x7D 0x5D
- 0x11 (XON) ? 0x7D 0x31
- 0x13 (XOFF) ? 0x7D 0x33
- 0x18 (SUBSTITUTE) ? 0x7D 0x38
- 0x1A (CANCEL) ? 0x7D 0x3A
- 0xF8 (RESERVED) ? 0x7D 0xD8

**Note:** All reserved bytes have bit 4 set to 1.

### CRC Expansion (3-byte CRC)

The 2-byte CRC-16 is expanded to 3 bytes to ensure bit 4 is 0 in all bytes (avoiding escaping):

1. Extract bit 4 from CRC byte 1 and byte 2
2. Clear bit 4 in both CRC bytes
3. Store cleared bytes as expanded CRC[0] and CRC[1]
4. Store the two bit-4 values in expanded CRC[2] bits 7 and 6

### Frame Counters

- **3-bit counters** numbered **1 through 7** (not 0-7!)
- OFC (Outgoing Frame Counter) - transmit sequence number
- AFC (ACK/NACK Frame Counter) - acknowledges received frames
- Automatic wrapping: 7 ? 1
- RESET frame uses OFC=1, AFC=0
- RESET_ACK uses OFC=1 (empty) or OFC=2 (with data), AFC=1

### Connection Flow

```
Host                NCP
 |                   |
 |------ RST ------->|  OFC=1, AFC=0
 |                   |
 |<---- RSTACK ------|  OFC=1, AFC=1 (empty payload)
 |                   |  or OFC=2, AFC=1 (with payload)
 |   (Connected)     |
 |                   |
 |---- ACK(1,1) ---->|  Host sends data
 |                   |
 |<---- ACK(1,2) ----|  NCP acknowledges
```

### Data Transfer

Per spec: **ACK, NACK, and RESET_ACK frames carry application data**. Empty ACKs/NACKs must use the same OFC as the last frame with data (OFC not incremented).

## Configuration

Default settings (per ASHv3 spec):

- **Window Size**: 2 (max 2 frames in flight)
- **Retransmission Timeout**: 500ms
- **Max Retries**: 3
- **Max Payload Length**: 57 bytes
- **Frame Counter Range**: 1-7
- **Default Baud Rate**: 115200
- **CRC**: 16-bit CRC-CCITT expanded to 3 bytes

## Testing

The Program.cs file includes an interactive test application:

```bash
dotnet run
```

Commands:
- `send <hex>` - Send hex data (e.g., "send 01 02 03")
- `quit` - Exit

## Requirements

- .NET 10
- System.IO.Ports NuGet package (10.0.0)
- Microsoft.Extensions.Logging (10.0.0)
- Microsoft.Extensions.Logging.Console (10.0.0)
- Serial port hardware or virtual port for testing

## Key Differences from Standard Protocols (v3 Specific)

1. **No DATA frame type** - data is carried in ACK/NACK/RESET_ACK frames (v3 only; v2 has DATA frames)
2. **4-byte header** - includes HEADER_ESCAPE byte (v3 only)
3. **3-byte CRC expansion** - unique to v3, ensures no CRC escaping
4. **Frame counters 1-7** - not 0-7 like many protocols (both v2 and v3)
5. **Empty ACKs don't increment OFC** - important for correct sequencing

**Note:** ASH v2 follows more traditional protocol patterns with DATA frames and 2-byte CRC.

## Reference

This implementation supports both:
- **ASH v2** - Silicon Labs UG101 specification
- **ASH v3** - Silicon Labs UG115 specification

Version is automatically detected from the NCP's RSTACK/RESET_ACK frame.
