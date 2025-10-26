using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Return the number of router children that the node currently has.
/// Frame value: 0x013B
/// </summary>
public class routerChildCount : EzspFrameRequest
{
}
