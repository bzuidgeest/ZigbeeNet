using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// This call is fired when a counter exceeds its threshold
/// Frame value: 0x00F2
/// </summary>
public class CounterRolloverHandlerRequest : EzspFrameRequest
{
}
