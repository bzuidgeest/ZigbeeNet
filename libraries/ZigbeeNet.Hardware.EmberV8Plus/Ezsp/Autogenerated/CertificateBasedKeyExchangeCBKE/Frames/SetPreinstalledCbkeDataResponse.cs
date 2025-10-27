using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Sets the device&apos;s CA public key, local certificate, and static private key on the NCP associated with this node.
/// Frame value: 0x00A2
/// </summary>
public class SetPreinstalledCbkeDataResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
