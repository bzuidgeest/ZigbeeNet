using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Calculates the SMAC verification keys for both the initiator and responder roles of CBKE for the 283k1 ECC curve using the passed parameters and the stored public/private key pair previously generated with sl_zigbee_ezsp_generate_keys_retrieve_cert_283k1(). It also stores the unverified link key data in temporary storage on the NCP until the key establishment is complete.
/// Frame value: 0x00EA
/// </summary>
public class CalculateSmacs283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
