using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Command;

/// <summary>
/// A callback by the Crypto Engine indicating that a new ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
/// Frame value: 0x009E
/// </summary>
public class generateCbkeKeysHandler : EzspFrameRequest
{
}
