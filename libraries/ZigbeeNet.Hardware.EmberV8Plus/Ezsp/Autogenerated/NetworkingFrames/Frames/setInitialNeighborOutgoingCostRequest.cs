using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Set initial outgoing link cost for neighbor.
/// Frame value: 0x0122
/// </summary>
public class setInitialNeighborOutgoingCost : EzspFrameRequest
{
    /// <summary>
    /// The new default cost. Valid values are 0, 1, 3, 5, and 7.
    /// </summary>
    public byte cost { get; set; }

}
