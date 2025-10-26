using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Convert a node ID to a child index
/// Frame value: 0x0107
/// </summary>
public class childIndex : EzspFrameRequest
{
    /// <summary>
    /// The node ID of the child
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

}
