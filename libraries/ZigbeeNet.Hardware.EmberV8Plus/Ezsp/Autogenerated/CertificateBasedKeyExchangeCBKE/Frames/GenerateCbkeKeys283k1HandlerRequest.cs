using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// A callback by the Crypto Engine indicating that a new 283k1 ephemeral public/private key pair has been generated. The public/private key pair is stored on the NCP, but only the associated public key is returned to the host. The node&apos;s associated certificate is also returned.
/// Frame value: 0x00E9
/// </summary>
public class GenerateCbkeKeys283k1HandlerRequest : EzspFrameRequest
{
}
