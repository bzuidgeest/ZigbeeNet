using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Returns the node ID that corresponds to the specified EUI64. The node ID is found by searching through all stack tables for the specified EUI64.
/// Frame value: 0x0060
/// </summary>
public class LookupNodeIdByEui64Response : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the short ID was found, SL_STATUS_FAIL if the short ID is not known.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The short ID of the node or SL_ZIGBEE_NULL_NODE_ID if the short ID is not known.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

}
