using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Returns the 16-bit node ID of the local node.
/// Frame value: 0x0027
/// </summary>
public class getNodeId : EzspFrameRequest
{
}
