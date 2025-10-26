using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Get initial outgoing link cost for neighbor.
/// Frame value: 0x0123
/// </summary>
public class getInitialNeighborOutgoingCostResponse : EzspFrameResponse
{
    /// <summary>
    /// The default cost associated with new neighbor&apos;s outgoing links.
    /// </summary>
    public byte cost { get; set; }

}
