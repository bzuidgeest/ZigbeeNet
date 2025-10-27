using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Retrieves the 283k certificate installed on the NCP.
/// Frame value: 0x00EC
/// </summary>
public class GetCertificate283k1Request : EzspFrameRequest
{
}
