using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Sets the device&apos;s 283k1 curve CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00ED
/// </summary>
public class savePreinstalledCbkeData283k1Response : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
