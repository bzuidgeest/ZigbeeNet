using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Return the maximum number of router children for this node. The return value is undefined for nodes that are not joined to a network.
/// Frame value: 0x013D
/// </summary>
public class maxRouterChildCountResponse : EzspFrameResponse
{
    /// <summary>
    /// The maximum number of router children.
    /// </summary>
    public byte maxRouterChildCount { get; set; }

}
