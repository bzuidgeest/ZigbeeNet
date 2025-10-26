using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Export a transient link key associated with a given EUI64
/// Frame value: 0x0113
/// </summary>
public class secManExportTransientKeyByEuiResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key export operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Context struct for export operation.
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
