using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// This call starts the generation of the ECC 283k1 curve Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_283k1_handler().
/// Frame value: 0x00E8
/// </summary>
public class GenerateCbkeKeys283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
