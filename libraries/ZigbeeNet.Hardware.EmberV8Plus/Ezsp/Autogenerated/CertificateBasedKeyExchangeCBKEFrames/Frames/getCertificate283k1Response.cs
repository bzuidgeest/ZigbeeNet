using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Retrieves the 283k certificate installed on the NCP.
/// Frame value: 0x00EC
/// </summary>
public class getCertificate283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

    /// <summary>
    /// The locally installed certificate.
    /// </summary>
    public sl_zigbee_certificate_283k1_data_t localCert { get; set; }

}
