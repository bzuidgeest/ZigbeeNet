using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Supply a source route for the next outgoing message.
/// Frame value: 0x00AE
/// </summary>
public class SetSourceRouteRequest : EzspFrameRequest
{
    /// <summary>
    /// The destination of the source route.
    /// </summary>
    public sl_802154_short_addr_t destination { get; set; }

    /// <summary>
    /// The number of relays in &lt;i&gt;relayList&lt;/i&gt;.
    /// </summary>
    public byte relayCount { get; set; }

    /// <summary>
    /// The source route.
    /// </summary>
    public uint16_t[relayCount] relayList { get; set; }

}
