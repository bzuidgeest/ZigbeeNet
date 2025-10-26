namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// An entry in the binding table.
/// </summary>
public struct sl_zigbee_binding_table_entry_t
{
    /// <summary>
    /// The type of binding.
    /// </summary>
    public sl_zigbee_binding_type_t type;

    /// <summary>
    /// The endpoint on the local node.
    /// </summary>
    public byte local;

    /// <summary>
    /// A cluster ID that matches one from the local endpoint's simple descriptor. This cluster ID is set by the provisioning application to indicate which part an endpoint's functionality is bound to this particular remote node and is used to distinguish between unicast and multicast bindings. Note that a binding can be used to send messages with any cluster ID, not just the one listed in the binding.
    /// </summary>
    public ushort clusterId;

    /// <summary>
    /// The endpoint on the remote node (specified by identifier).
    /// </summary>
    public byte remote;

    /// <summary>
    /// A 64-bit identifier. This is either the destination EUI64 (for unicasts) or the 64-bit group address (for multicasts).
    /// </summary>
    public sl_802154_long_addr_t identifier;

    /// <summary>
    /// The index of the network the binding belongs to.
    /// </summary>
    public byte networkIndex;

}

