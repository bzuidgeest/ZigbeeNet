using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Retrieves the certificate installed on the NCP.
/// Frame value: 0x00A5
/// </summary>
public class getCertificateResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

    /// <summary>
    /// The locally installed certificate.
    /// </summary>
    public sl_zigbee_certificate_data_t localCert { get; set; }

}
