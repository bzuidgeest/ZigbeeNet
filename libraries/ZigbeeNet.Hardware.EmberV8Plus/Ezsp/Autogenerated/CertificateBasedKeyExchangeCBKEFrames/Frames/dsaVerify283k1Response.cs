using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
/// Frame value: 0x00B0
/// </summary>
public class dsaVerify283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
