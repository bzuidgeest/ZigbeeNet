using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
/// Frame value: 0x00B0
/// </summary>
public class DsaVerify283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
