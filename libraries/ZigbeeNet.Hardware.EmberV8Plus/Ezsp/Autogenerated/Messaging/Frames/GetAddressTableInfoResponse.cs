using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Gets the EUI64 and short ID of an address table entry.
/// Frame value: 0x005E
/// </summary>
public class GetAddressTableInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// One of the following: The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index. SL_ZIGBEE_UNKNOWN_NODE_ID - Indicates that the EUI64 stored in the address table at the given index is valid but the short ID is currently unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID - Indicates that the EUI64 stored in the address table at the given location is valid and network address discovery is underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID - Indicates that the entry stored in the address table at the given index is not in use.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

    /// <summary>
    /// The EUI64 of the address table entry is copied to this location.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

}
