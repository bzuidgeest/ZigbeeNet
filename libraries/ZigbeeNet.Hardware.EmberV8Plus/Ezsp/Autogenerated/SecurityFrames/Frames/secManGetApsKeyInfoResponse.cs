using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Retrieve metadata about an APS link key.  Does not retrieve contents.
/// Frame value: 0x010C
/// </summary>
public class secManGetApsKeyInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of metadata retrieval operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Metadata about the referenced key.
    /// </summary>
    public sl_zigbee_sec_man_aps_key_metadata_t key_data { get; set; }

}
