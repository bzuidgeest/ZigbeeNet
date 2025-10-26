using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Replaces the EUI64, short ID and extended timeout setting of an address table entry. The previous EUI64, short ID and extended timeout setting are returned.
/// Frame value: 0x0082
/// </summary>
public class replaceAddressTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the EUI64, short ID and extended timeout setting were successfully modified, and SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The EUI64 of the address table entry before it was modified.
    /// </summary>
    public sl_802154_long_addr_t oldEui64 { get; set; }

    /// <summary>
    /// One of the following: The short ID corresponding to the EUI64 before it was modified. SL_ZIGBEE_UNKNOWN_NODE_ID if the short ID was unknown. SL_ZIGBEE_DISCOVERY_ACTIVE_NODE_ID if discovery of the short ID was underway. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID if the address table entry was unused.
    /// </summary>
    public sl_802154_short_addr_t oldId { get; set; }

    /// <summary>
    /// true if the retry interval was being increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval was being used.
    /// </summary>
    public bool oldExtendedTimeout { get; set; }

}
