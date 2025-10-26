using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Tells the stack whether or not the normal interval between retransmissions of a retried unicast message should be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT. The interval needs to be increased when sending to a sleepy node so that the message is not retransmitted until the destination has had time to wake up and poll its parent. The stack will automatically extend the timeout: - For our own sleepy children. - When an address response is received from a parent on behalf of its child. - When an indirect transaction expiry route error is received. - When an end device announcement is received from a sleepy node.
/// Frame value: 0x007E
/// </summary>
public class setExtendedTimeoutResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure  
    /// </summary>
    public sl_status_t status { get; set; }

}
