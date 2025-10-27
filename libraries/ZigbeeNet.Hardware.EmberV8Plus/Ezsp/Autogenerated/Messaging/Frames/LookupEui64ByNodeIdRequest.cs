using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Returns the EUI64 that corresponds to the specified node ID. The EUI64 is found by searching through all stack tables for the specified node ID.
/// Frame value: 0x0061
/// </summary>
public class LookupEui64ByNodeIdRequest : EzspFrameRequest
{
    /// <summary>
    /// The short ID of the node to look up.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

}
