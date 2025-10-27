using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Indicates the result of a data poll to the parent of the local node.
/// Frame value: 0x0043
/// </summary>
public class PollCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value: SL_STATUS_OK - Data was received in response to the poll. SL_STATUS_MAC_NO_DATA - No data was pending. SL_STATUS_ZIGBEE_DELIVERY_FAILED - The poll message could not be sent. SL_STATUS_MAC_NO_ACK_RECEIVED - The poll message was sent but not acknowledged by the parent.
    /// </summary>
    public sl_status_t status { get; set; }

}
