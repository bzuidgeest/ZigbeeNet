using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// A callback by the Crypto Engine indicating that a new 283k1 ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
/// Frame value: 0x00E9
/// </summary>
public class generateCbkeKeys283k1HandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The result of the CBKE operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The generated ephemeral public key.
    /// </summary>
    public sl_zigbee_public_key_283k1_data_t ephemeralPublicKey { get; set; }

}
