using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// A callback invoked by the EmberZNet stack when a MAC passthrough message is received.
/// Frame value: 0x0097
/// </summary>
public class macPassthroughMessageHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The type of MAC passthrough message received.
    /// </summary>
    public sl_zigbee_mac_passthrough_type_t messageType { get; set; }

    /// <summary>
    /// Information about the incoming packet.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The raw message that was received.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
