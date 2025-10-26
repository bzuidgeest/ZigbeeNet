using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Remove a neighbor from neighbor table only on SoC, allowing direct manipulation of neighbor table by the application. This can affect the network functionality, and needs to be used wisely.
/// Frame value: 0x013A
/// </summary>
public class removeNeighbor : EzspFrameRequest
{
    /// <summary>
    /// The short ID of the neighbor.
    /// </summary>
    public sl_802154_short_addr_t shortId { get; set; }

    /// <summary>
    /// The long ID of the neighbor.
    /// </summary>
    public sl_802154_long_addr_t longId { get; set; }

}
