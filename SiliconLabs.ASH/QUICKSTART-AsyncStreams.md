# Quick Start: Using Async Streams with AshHost

## Basic Usage

### 1. Continuous Stream Processing (Recommended)

```csharp
using var ashHost = new AshHost("COM3", 115200, loggerFactory, AshVersion.V3);

await ashHost.ConnectAsync(5000);

// Process payloads continuously
await foreach (var payload in ashHost.GetPayloadStream())
{
    Console.WriteLine($"Received: {AshHost.ToHexString(payload)}");
}
```

---

### 2. Ember Protocol Parsing (Multi-Frame Messages)

```csharp
var emberBuffer = new List<byte>();

await foreach (var ashPayload in ashHost.GetPayloadStream(cancellationToken))
{
    // Accumulate ASH payloads into continuous byte stream
    emberBuffer.AddRange(ashPayload);
    
    // Extract complete Ember messages
    while (TryParseEmberMessage(emberBuffer, out var message))
    {
        await HandleEmberMessage(message);
    }
}

bool TryParseEmberMessage(List<byte> buffer, out byte[] message)
{
    // Your Ember/EZSP parsing logic here
    // Return true if complete message found
    // Remove parsed bytes from buffer
}
```

---

### 3. With Cancellation and Timeout

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));

try
{
    await foreach (var payload in ashHost.GetPayloadStream(cts.Token))
    {
        ProcessPayload(payload);
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Cancelled or timeout");
}
```

---

### 4. Parallel Processing

```csharp
var task1 = Task.Run(async () =>
{
    await foreach (var payload in ashHost.GetPayloadStream())
    {
        await ProcessPayloadAsync(payload, "Worker1");
    }
});

var task2 = Task.Run(async () =>
{
    await foreach (var payload in ashHost.GetPayloadStream())
    {
        await ProcessPayloadAsync(payload, "Worker2");
    }
});

await Task.WhenAll(task1, task2);
```

---

## Configuration

### Adjust Channel Buffer Size

```csharp
// Default: 100 payloads
var ashHost = new AshHost(port, baud, ...);

// High throughput: larger buffer
var ashHost = new AshHost(port, baud, ..., payloadChannelCapacity: 1000);
```

---

## Complete Example

```csharp
using SiliconLabs.ASH.AshProtocol;
using Microsoft.Extensions.Logging;

// Setup logging
using var loggerFactory = LoggerFactory.Create(builder =>
    builder.AddConsole().SetMinimumLevel(LogLevel.Information));

// Create and connect with explicit version configuration
using var ashHost = new AshHost("COM3", 115200, loggerFactory, AshVersion.V3);

// Monitor backpressure
ashHost.ChannelFull += (count) =>
{
    Console.WriteLine($"??  Channel full! {count} items - consumer too slow!");
};

if (!await ashHost.ConnectAsync(5000))
{
    Console.WriteLine("Failed to connect");
    return;
}

Console.WriteLine("Connected! Processing payloads...");
