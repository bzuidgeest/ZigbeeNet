using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Replaces the EUI64, short ID and extended timeout setting of an address table entry. The previous EUI64, short ID and extended timeout setting are returned.
/// Frame value: 0x0082
/// </summary>
public class replaceAddressTableEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the address table entry that will be modified.
    /// </summary>
    public byte addressTableIndex { get; set; }

    /// <summary>
    /// The EUI64 to be written to the address table entry.
    /// </summary>
    public sl_802154_long_addr_t newEui64 { get; set; }

    /// <summary>
    /// One of the following: The short ID corresponding to the new EUI64. SL_ZIGBEE_UNKNOWN_NODE_ID if the new EUI64 is valid but the short ID is unknown and should be discovered by the stack. SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID if the address table entry is now unused.
    /// </summary>
    public sl_802154_short_addr_t newId { get; set; }

    /// <summary>
    /// true if the retry interval should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. false if the normal retry interval should be used.
    /// </summary>
    public bool newExtendedTimeout { get; set; }

}
