using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Convert a child index to a node ID
/// Frame value: 0x0106
/// </summary>
public class ChildIdResponse : EzspFrameResponse
{
    /// <summary>
    /// The node ID of the child or SL_ZIGBEE_NULL_NODE_ID if there isn&apos;t a child at the childIndex specified
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

}
