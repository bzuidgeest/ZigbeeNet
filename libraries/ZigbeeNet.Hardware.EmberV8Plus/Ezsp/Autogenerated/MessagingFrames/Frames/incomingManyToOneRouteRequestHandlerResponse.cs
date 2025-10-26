using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// A callback indicating that a many-to-one route to the concentrator with the given short and long id is available for use.
/// Frame value: 0x007D
/// </summary>
public class incomingManyToOneRouteRequestHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The short id of the concentrator.
    /// </summary>
    public sl_802154_short_addr_t source { get; set; }

    /// <summary>
    /// The EUI64 of the concentrator.
    /// </summary>
    public sl_802154_long_addr_t longId { get; set; }

    /// <summary>
    /// The path cost to the concentrator. The cost may decrease as additional route request packets for this discovery arrive, but the callback is made only once.
    /// </summary>
    public byte cost { get; set; }

}
