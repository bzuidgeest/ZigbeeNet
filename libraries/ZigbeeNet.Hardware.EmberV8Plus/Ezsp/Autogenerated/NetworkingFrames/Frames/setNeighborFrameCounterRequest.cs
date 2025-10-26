using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the frame counter for the neighbour or child.
/// Frame value: 0x00AD
/// </summary>
public class setNeighborFrameCounter : EzspFrameRequest
{
    /// <summary>
    /// eui64 of the node
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

    /// <summary>
    /// Return the frame counter of the node from the neighbor or child table
    /// </summary>
    public uint frameCounter { get; set; }

}
