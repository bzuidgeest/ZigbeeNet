using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns the number of filled entries in source route table.
/// Frame value: 0x00C2
/// </summary>
public class getSourceRouteTableFilledSize : EzspFrameRequest
{
}
