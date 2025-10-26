using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Indicates that a child has joined or left.
/// Frame value: 0x0023
/// </summary>
public class childJoinHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The index of the child of interest.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// True if the child is joining. False the child is leaving.
    /// </summary>
    public bool joining { get; set; }

    /// <summary>
    /// The node ID of the child.
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

    /// <summary>
    /// The EUI64 of the child.
    /// </summary>
    public sl_802154_long_addr_t childEui64 { get; set; }

    /// <summary>
    /// The node type of the child.
    /// </summary>
    public sl_zigbee_node_type_t childType { get; set; }

}
