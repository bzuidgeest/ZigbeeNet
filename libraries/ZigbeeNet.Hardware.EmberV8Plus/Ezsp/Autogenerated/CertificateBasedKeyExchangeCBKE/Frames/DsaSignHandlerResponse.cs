using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// The handler that returns the results of the signing operation. On success, the signature will be appended to the original message (including the signature type indicator that replaced the startIndex field for the signing) and both are returned via this callback.
/// Frame value: 0x00A7
/// </summary>
public class DsaSignHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The result of the DSA signing operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The message and attached which includes the original message and the appended signature.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
