using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Convert a node ID to a child index
/// Frame value: 0x0107
/// </summary>
public class ChildIndexRequest : EzspFrameRequest
{
    /// <summary>
    /// The node ID of the child
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

}
