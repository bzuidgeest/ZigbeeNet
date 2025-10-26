using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns information about the children of the local node and the parent of the local node.
/// Frame value: 0x0029
/// </summary>
public class getParentChildParameters : EzspFrameRequest
{
}
