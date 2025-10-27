using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Indicates whether or not the stack will extend the normal interval between retransmissions of a retried unicast message by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT.
/// Frame value: 0x007F
/// </summary>
public class GetExtendedTimeoutResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the retry interval will be increased by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT and SL_STATUS_FAIL if the normal retry interval will be used.
    /// </summary>
    public sl_status_t status { get; set; }

}
