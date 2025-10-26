using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Export the link key at given index from the key table.
/// Frame value: 0x010F
/// </summary>
public class secManExportLinkKeyByIndexResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key export operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Context referencing the exported key.  Contains information like the EUI64 address it is associated with.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

    /// <summary>
    /// The exported key.
    /// </summary>
    public sl_zigbee_sec_man_key_t plaintext_key { get; set; }

    /// <summary>
    /// Metadata about the key.
    /// </summary>
    public sl_zigbee_sec_man_aps_key_metadata_t key_data { get; set; }

}
