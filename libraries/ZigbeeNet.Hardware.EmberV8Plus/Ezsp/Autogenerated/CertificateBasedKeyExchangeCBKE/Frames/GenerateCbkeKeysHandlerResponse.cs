using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// A callback by the Crypto Engine indicating that a new ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
/// Frame value: 0x009E
/// </summary>
public class GenerateCbkeKeysHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The result of the CBKE operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The generated ephemeral public key.
    /// </summary>
    public sl_zigbee_public_key_data_t ephemeralPublicKey { get; set; }

}
