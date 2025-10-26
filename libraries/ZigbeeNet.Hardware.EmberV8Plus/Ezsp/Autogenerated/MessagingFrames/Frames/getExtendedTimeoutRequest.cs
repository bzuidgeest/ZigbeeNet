using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Indicates whether or not the stack will extend the normal interval between retransmissions of a retried unicast message by SL_ZIGBEE_INDIRECT_TRANSMISSION_TIMEOUT.
/// Frame value: 0x007F
/// </summary>
public class getExtendedTimeout : EzspFrameRequest
{
    /// <summary>
    /// The address of the node for which the timeout is to be returned.
    /// </summary>
    public sl_802154_long_addr_t remoteEui64 { get; set; }

}
