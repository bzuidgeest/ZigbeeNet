using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// A callback to indicate that the NCP has finished calculating the Secure Message Authentication Codes (SMAC) for both the initiator and responder for the CBKE 283k1 Library. The associated link key is kept in temporary storage until the host tells the NCP to store or discard the key via sli_zigbee_stack_clear_temporary_data_maybe_store_link_key().
/// Frame value: 0x00EB
/// </summary>
public class calculateSmacs283k1HandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The Result of the CBKE operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The calculated value of the initiator&apos;s SMAC
    /// </summary>
    public sl_zigbee_smac_data_t initiatorSmac { get; set; }

    /// <summary>
    /// The calculated value of the responder&apos;s SMAC
    /// </summary>
    public sl_zigbee_smac_data_t responderSmac { get; set; }

}
