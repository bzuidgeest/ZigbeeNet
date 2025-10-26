using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns the number of active entries in the neighbor table.
/// Frame value: 0x007A
/// </summary>
public class neighborCount : EzspFrameRequest
{
}
