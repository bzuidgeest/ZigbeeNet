using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sets the EUI64 and short ID of an address table entry. Usually the application will not need to set the short ID in the address table. Once the remote EUI64 is set the stack is capable of figuring out the short ID on its own. However, in cases where the application does set the short ID, the application must set the remote EUI64 prior to setting the short ID. This function will also check other address table entries, the child table and the neighbor table to see if the node ID for the given EUI64 is already known. If known then this function will set node ID. If not known it will set the node ID to SL_ZIGBEE_UNKNOWN_NODE_ID.
/// Frame value: 0x005C
/// </summary>
public class SetAddressTableInfoRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of an address table entry.
    /// </summary>
    public byte addressTableIndex { get; set; }

    /// <summary>
    /// The EUI64 to use for the address table entry.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

    /// <summary>
    /// The short ID corresponding to the remote node whose EUI64 is stored in the address table at the given index or SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID which indicates that the entry stored in the address table at the given index is not in use.
    /// </summary>
    public sl_802154_short_addr_t id { get; set; }

}
