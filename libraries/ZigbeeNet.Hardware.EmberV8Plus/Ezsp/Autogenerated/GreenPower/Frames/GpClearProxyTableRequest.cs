using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Clear the entire proxy table
/// Frame value: 0x005F
/// </summary>
public class GpClearProxyTableRequest : EzspFrameRequest
{
}
