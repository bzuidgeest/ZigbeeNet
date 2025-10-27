using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE using the passed parameters and the stored public/private key pair previously generated with ezspGenerateKeysRetrieveCert(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
/// Frame value: 0x009F
/// </summary>
public class CalculateSmacsResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
