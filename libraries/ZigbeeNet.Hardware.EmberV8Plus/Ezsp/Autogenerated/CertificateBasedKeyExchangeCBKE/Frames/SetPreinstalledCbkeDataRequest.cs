using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Sets the device&apos;s CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00A2
/// </summary>
public class SetPreinstalledCbkeDataRequest : EzspFrameRequest
{
    /// <summary>
    /// The Certificate Authority&apos;s public key.
    /// </summary>
    public sl_zigbee_public_key_data_t caPublic { get; set; }

    /// <summary>
    /// The node&apos;s new certificate signed by the CA.
    /// </summary>
    public sl_zigbee_certificate_data_t myCert { get; set; }

    /// <summary>
    /// The node&apos;s new static private key.
    /// </summary>
    public sl_zigbee_private_key_data_t myKey { get; set; }

}
