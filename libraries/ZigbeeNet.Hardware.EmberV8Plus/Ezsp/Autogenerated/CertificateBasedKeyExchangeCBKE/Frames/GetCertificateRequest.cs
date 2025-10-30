using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Retrieves the certificate installed on the NCP.
/// Frame value: 0x00A5
/// </summary>
public class GetCertificateRequest : EzspFrameRequest
{
