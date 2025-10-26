using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenterFrames.Structure;

/// <summary>
/// This routine processes the passed chunk of data and updates the hash context based on it. If the &apos;finalize&apos; parameter is not set, then the length of the data passed in must be a multiple of 16. If the &apos;finalize&apos; parameter is set then the length can be any value up 1-16, and the final hash value will be calculated.
/// Frame value: 0x006F
/// </summary>
public class aesMmoHashResponse : EzspFrameResponse
{
    /// <summary>
    /// The result of the operation
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The updated hash context.
    /// </summary>
    public sl_zigbee_aes_mmo_hash_context_t returnContext { get; set; }

}
