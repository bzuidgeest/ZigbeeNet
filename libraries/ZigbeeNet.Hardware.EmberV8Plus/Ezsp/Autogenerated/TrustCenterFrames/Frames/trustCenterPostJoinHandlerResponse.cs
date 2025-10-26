using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenterFrames.Structure;

/// <summary>
/// The NCP uses the trust center behavior policy to decide whether to allow a new node to join the network (part of the trust center pre-join handler). The Host cannot change the current decision in this post-join callback, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
/// Frame value: 0x0024
/// </summary>
public class trustCenterPostJoinHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The Node Id of the node whose status changed
    /// </summary>
    public sl_802154_short_addr_t newNodeId { get; set; }

    /// <summary>
    /// The EUI64 of the node whose status changed.
    /// </summary>
    public sl_802154_long_addr_t newNodeEui64 { get; set; }

    /// <summary>
    /// The status of the node: Secure Join/Rejoin, Unsecure Join/Rejoin, Device left.
    /// </summary>
    public sl_zigbee_device_update_t status { get; set; }

    /// <summary>
    /// An sl_zigbee_join_decision_t reflecting the decision made.
    /// </summary>
    public sl_zigbee_join_decision_t policyDecision { get; set; }

    /// <summary>
    /// The parent of the node whose status has changed.
    /// </summary>
    public sl_802154_short_addr_t parentOfNewNodeId { get; set; }

}
