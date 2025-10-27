using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Used to test that UART flow control is working correctly.
/// Frame value: 0x009D
/// </summary>
public class DelayTestResponse : EzspFrameResponse
{
}
