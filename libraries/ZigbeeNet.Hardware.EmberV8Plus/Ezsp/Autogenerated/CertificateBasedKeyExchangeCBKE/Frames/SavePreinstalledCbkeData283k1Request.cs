using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Sets the device&apos;s 283k1 curve CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00ED
/// </summary>
public class SavePreinstalledCbkeData283k1Request : EzspFrameRequest
{
}
