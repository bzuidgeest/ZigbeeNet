using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Command;

/// <summary>
/// Retrieves the 283k certificate installed on the NCP.
/// Frame value: 0x00EC
/// </summary>
public class getCertificate283k1 : EzspFrameRequest
{
}
