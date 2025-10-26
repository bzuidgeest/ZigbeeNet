using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// LEGACY FUNCTION: This functionality has been replaced by a single bit in the sl_zigbee_aps_frame_t, SL_ZIGBEE_APS_OPTION_DSA_SIGN. Devices wishing to send signed messages should use that as it requires fewer function calls and message buffering. The dsaSignHandler response is still called when SL_ZIGBEE_APS_OPTION_DSA_SIGN is used. However, this function is still supported. This function begins the process of signing the passed message contained within the messageContents array. If no other ECC operation is going on, it will immediately return with SL_STATUS_IN_PROGRESS to indicate the start of ECC operation. It will delay a period of time to let APS retries take place, but then it will shut down the radio and consume the CPU processing until the signing is complete. This may take up to 1 second. The signed message will be returned in the dsaSignHandler response. Note that the last byte of the messageContents passed to this function has special significance. As the typical use case for DSA signing is to sign the ZCL payload of a DRLC Report Event Status message in SE 1.0, there is often both a signed portion (ZCL payload) and an unsigned portion (ZCL header). The last byte in the content of messageToSign is therefore used as a special indicator to signify how many bytes of leading data in the array should be excluded from consideration during the signing process. If the signature needs to cover the entire array (all bytes except last one), the caller should ensure that the last byte of messageContents is 0x00. When the signature operation is complete, this final byte will be replaced by the signature type indicator (0x01 for ECDSA signatures), and the actual signature will be appended to the original contents after this byte.
/// Frame value: 0x00A6
/// </summary>
public class dsaSignResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_IN_PROGRESS if the stack has queued up the operation for execution. SL_STATUS_INVALID_STATE if the operation can&apos;t be performed in this context, possibly because another ECC operation is pending.
    /// </summary>
    public sl_status_t status { get; set; }

}
