using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Add a child to the child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely. If used appropriately, the application can maintain more than the maximum of children provided by the stack.
/// Frame value: 0x0138
/// </summary>
public class addChild : EzspFrameRequest
{
    /// <summary>
    /// The preferred short ID of the node.
    /// </summary>
    public sl_802154_short_addr_t shortId { get; set; }

    /// <summary>
    /// The long ID of the node.
    /// </summary>
    public sl_802154_long_addr_t longId { get; set; }

    /// <summary>
    /// The nodetype e.g., SL_ZIGBEE_ROUTER defining, if this would be added to the child table or neighbor table.
    /// </summary>
    public sl_zigbee_node_type_t nodeType { get; set; }

}
