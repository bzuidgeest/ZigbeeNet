using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Command;

/// <summary>
/// Retrieves the certificate installed on the NCP.
/// Frame value: 0x00A5
/// </summary>
public class getCertificate : EzspFrameRequest
{
}
