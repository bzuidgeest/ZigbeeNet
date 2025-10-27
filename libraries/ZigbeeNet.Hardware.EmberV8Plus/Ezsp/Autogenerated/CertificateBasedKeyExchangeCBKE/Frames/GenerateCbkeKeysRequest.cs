using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// This call starts the generation of the ECC Ephemeral Public/Private key pair. When complete it stores the private key. The results are returned via sl_zigbee_ezsp_generate_cbke_keys_handler().
/// Frame value: 0x00A4
/// </summary>
public class GenerateCbkeKeysRequest : EzspFrameRequest
{
}
