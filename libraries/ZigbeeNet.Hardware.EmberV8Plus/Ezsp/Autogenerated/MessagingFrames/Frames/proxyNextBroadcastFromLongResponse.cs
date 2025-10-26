using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Sends proxied broadcast message for another node in conjunction with sl_zigbee_proxy_broadcast where a long source is also specified in the NWK frame control.
/// Frame value: 0x0066
/// </summary>
public class proxyNextBroadcastFromLongResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
