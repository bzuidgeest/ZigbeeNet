using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Returns the EUI64 that corresponds to the specified node ID. The EUI64 is found by searching through all stack tables for the specified node ID.
/// Frame value: 0x0061
/// </summary>
public class lookupEui64ByNodeIdResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the EUI64 was found, SL_STATUS_FAIL if the EUI64 is not known.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The EUI64 of the node.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

}
