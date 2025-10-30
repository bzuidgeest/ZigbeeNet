using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Remove a neighbor from neighbor table only on SoC, allowing direct manipulation of neighbor table by the application. This can affect the network functionality, and needs to be used wisely.
/// Frame value: 0x013A
/// </summary>
public class RemoveNeighborRequest : EzspFrameRequest
{
    /// <summary>
    /// The short ID of the neighbor.
    /// </summary>
    public sl_802154_short_addr_t shortId { get; set; }

    /// <summary>
    /// The long ID of the neighbor.
    /// </summary>
    public sl_802154_long_addr_t longId { get; set; }

