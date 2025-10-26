using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Returns the 16-bit node ID of the local node.
/// Frame value: 0x0027
/// </summary>
public class getNodeIdResponse : EzspFrameResponse
{
    /// <summary>
    /// The 16-bit ID.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

}
