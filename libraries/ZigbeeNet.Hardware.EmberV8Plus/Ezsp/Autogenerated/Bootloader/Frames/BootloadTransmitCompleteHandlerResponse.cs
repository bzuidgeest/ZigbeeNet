using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a bootload message.
/// Frame value: 0x0093
/// </summary>
public class BootloadTransmitCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value of SL_STATUS_OK if an ACK was received from the destination or SL_STATUS_ZIGBEE_DELIVERY_FAILED if no ACK was received.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The message that was sent.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
