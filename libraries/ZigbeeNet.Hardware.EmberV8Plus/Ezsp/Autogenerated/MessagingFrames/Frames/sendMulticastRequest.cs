using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Sends a multicast message to all endpoints that share a specific multicast ID and are within a specified number of hops of the sender.
/// Frame value: 0x0038
/// </summary>
public class sendMulticast : EzspFrameRequest
{
    /// <summary>
    /// The APS frame for the message. The multicast will be sent to the groupId in this frame.
    /// </summary>
    public sl_zigbee_aps_frame_t apsFrame { get; set; }

    /// <summary>
    /// The message will be delivered to all nodes within this number of hops of the sender. A value of zero is converted to SL_ZIGBEE_MAX_HOPS.
    /// </summary>
    public byte hops { get; set; }

    /// <summary>
    /// The number of hops that the message will be forwarded by devices that are not members of the group. A value of 7 or greater is treated as infinite.
    /// </summary>
    public ushort broadcastAddr { get; set; }

    /// <summary>
    /// The alias source address
    /// </summary>
    public ushort alias { get; set; }

    /// <summary>
    /// the alias sequence number
    /// </summary>
    public byte nwkSequence { get; set; }

    /// <summary>
    /// A value chosen by the Host. This value is used in the &lt;i&gt;sl_zigbee_ezsp_message_sent_handler&lt;/i&gt; response to refer to this message.
    /// </summary>
    public ushort messageTag { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The multicast message.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
