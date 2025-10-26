using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Sets the device&apos;s CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00A2
/// </summary>
public class setPreinstalledCbkeDataResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
