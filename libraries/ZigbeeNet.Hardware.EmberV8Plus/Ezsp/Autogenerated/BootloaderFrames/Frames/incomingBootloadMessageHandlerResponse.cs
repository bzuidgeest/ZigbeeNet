using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// A callback invoked by the EmberZNet stack when a bootload message is received.
/// Frame value: 0x0092
/// </summary>
public class incomingBootloadMessageHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The EUI64 of the sending node.
    /// </summary>
    public sl_802154_long_addr_t longId { get; set; }

    /// <summary>
    /// Information about the incoming packet.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The bootload message that was sent.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
