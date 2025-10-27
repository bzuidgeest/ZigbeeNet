using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Transmits the given message without modification. The MAC header is assumed to be configured in the message at the time this function is called.
/// Frame value: 0x0051
/// </summary>
public class SendRawMessageRequest : EzspFrameRequest
{
    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The raw message.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

    /// <summary>
    /// transmit priority.
    /// </summary>
    public byte priority { get; set; }

    /// <summary>
    /// Should we enable CCA or not.
    /// </summary>
    public bool useCca { get; set; }

}
