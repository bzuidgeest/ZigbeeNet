using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Command;

/// <summary>
/// Sets the device&apos;s 283k1 curve CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00ED
/// </summary>
public class savePreinstalledCbkeData283k1 : EzspFrameRequest
{
}
