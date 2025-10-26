using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Command;

/// <summary>
/// This callback is executed by the stack when the DSA verification has completed and has a result. If the result is SL_STATUS_OK, the signature is valid. If the result is SL_STATUS_ZIGBEE_SIGNATURE_VERIFY_FAILURE then the signature is invalid. If the result is anything else then the signature verify operation failed and the validity is unknown.
/// Frame value: 0x0078
/// </summary>
public class dsaVerifyHandler : EzspFrameRequest
{
}
