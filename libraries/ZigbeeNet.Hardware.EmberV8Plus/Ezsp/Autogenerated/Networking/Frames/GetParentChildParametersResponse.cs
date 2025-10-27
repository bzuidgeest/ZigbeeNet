using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns information about the children of the local node and the parent of the local node.
/// Frame value: 0x0029
/// </summary>
public class GetParentChildParametersResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of children the node currently has.
    /// </summary>
    public byte childCount { get; set; }

    /// <summary>
    /// The parent&apos;s EUI64. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).
    /// </summary>
    public sl_802154_long_addr_t parentEui64 { get; set; }

    /// <summary>
    /// The parent&apos;s node ID. The value is undefined for nodes without parents (coordinators and nodes that are not joined to a network).
    /// </summary>
    public sl_802154_short_addr_t parentNodeId { get; set; }

}
