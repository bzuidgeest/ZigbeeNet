using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a raw message.
/// Frame value: 0x0098
/// </summary>
public class rawTransmitCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Length of the message that was transmitted.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The message that was transmitted.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

    /// <summary>
    /// SL_STATUS_OK if the transmission was successful, or SL_STATUS_ZIGBEE_DELIVERY_FAILED if not
    /// </summary>
    public sl_status_t status { get; set; }

}
