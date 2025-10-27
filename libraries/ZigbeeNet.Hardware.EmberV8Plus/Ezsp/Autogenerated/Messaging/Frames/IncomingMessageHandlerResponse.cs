using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback indicating a message has been received.
/// Frame value: 0x0045
/// </summary>
public class IncomingMessageHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The type of the incoming message. One of the following: SL_ZIGBEE_INCOMING_UNICAST, SL_ZIGBEE_INCOMING_UNICAST_REPLY, SL_ZIGBEE_INCOMING_MULTICAST, SL_ZIGBEE_INCOMING_MULTICAST_LOOPBACK, SL_ZIGBEE_INCOMING_BROADCAST, SL_ZIGBEE_INCOMING_BROADCAST_LOOPBACK
    /// </summary>
    public sl_zigbee_incoming_message_type_t type { get; set; }

    /// <summary>
    /// The APS frame from the incoming message.
    /// </summary>
    public sl_zigbee_aps_frame_t apsFrame { get; set; }

    /// <summary>
    /// Miscellanous message information.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;message&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The incoming message.
    /// </summary>
    public uint8_t[messageLength] message { get; set; }

}
