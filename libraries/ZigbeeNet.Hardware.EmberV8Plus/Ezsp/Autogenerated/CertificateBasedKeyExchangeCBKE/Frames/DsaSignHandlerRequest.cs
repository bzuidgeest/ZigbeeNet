using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// The handler that returns the results of the signing operation. On success, the signature will be appended to the original message (including the signature type indicator that replaced the startIndex field for the signing) and both are returned via this callback.
/// Frame value: 0x00A7
/// </summary>
public class DsaSignHandlerRequest : EzspFrameRequest
{
}
