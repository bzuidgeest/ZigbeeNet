using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Returns the node ID that corresponds to the specified EUI64. The node ID is found by searching through all stack tables for the specified EUI64.
/// Frame value: 0x0060
/// </summary>
public class lookupNodeIdByEui64 : EzspFrameRequest
{
    /// <summary>
    /// The EUI64 of the node to look up.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

}
