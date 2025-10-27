using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when a raw MAC message that has matched one of the application&apos;s configured MAC filters.
/// Frame value: 0x0046
/// </summary>
public class MacFilterMatchMessageHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The value of the filter that was matched.
    /// </summary>
    public sl_zigbee_mac_filter_match_data_t filterValueMatch { get; set; }

    /// <summary>
    /// The type of MAC passthrough message received.
    /// </summary>
    public sl_zigbee_mac_passthrough_type_t legacyPassthroughType { get; set; }

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
