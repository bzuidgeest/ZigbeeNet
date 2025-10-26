namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// A structure containing a child node's data.
/// </summary>
public struct sl_zigbee_child_data_t
{
    /// <summary>
    /// The EUI64 of the child
    /// </summary>
    public sl_802154_long_addr_t eui64;

    /// <summary>
    /// The node type of the child
    /// </summary>
    public sl_zigbee_node_type_t type;

    /// <summary>
    /// The short address of the child
    /// </summary>
    public sl_802154_short_addr_t id;

    /// <summary>
    /// The phy of the child
    /// </summary>
    public byte phy;

    /// <summary>
    /// The power of the child
    /// </summary>
    public byte power;

    /// <summary>
    /// The timeout of the child
    /// </summary>
    public byte timeout;

}

