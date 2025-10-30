using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sends a reply to a received unicast message. The &lt;i&gt;incomingMessageHandler&lt;/i&gt; callback for the unicast being replied to supplies the values for all the parameters except the reply itself.
/// Frame value: 0x0039
/// </summary>
public class SendReplyRequest : EzspFrameRequest
{
    /// <summary>
    /// Value supplied by incoming unicast.
    /// </summary>
    public sl_802154_short_addr_t sender { get; set; }

    /// <summary>
    /// Value supplied by incoming unicast.
    /// </summary>
    public sl_zigbee_aps_frame_t apsFrame { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The reply message.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

