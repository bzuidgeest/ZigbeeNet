using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
/// Frame value: 0x00A3
/// </summary>
public class DsaVerifyResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
