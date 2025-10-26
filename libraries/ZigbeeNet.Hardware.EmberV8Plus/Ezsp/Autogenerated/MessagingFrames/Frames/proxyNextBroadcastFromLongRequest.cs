using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Sends proxied broadcast message for another node in conjunction with sl_zigbee_proxy_broadcast where a long source is also specified in the NWK frame control.
/// Frame value: 0x0066
/// </summary>
public class proxyNextBroadcastFromLong : EzspFrameRequest
{
    /// <summary>
    /// The long source from which to send the broadcast
    /// </summary>
    public uint8_t[8] euiSource { get; set; }

}
