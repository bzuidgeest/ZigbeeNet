namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The parameters of a ZLL network.
/// </summary>
public struct sl_zigbee_zll_network_t
{
    /// <summary>
    /// The parameters of a ZigBee network.
    /// </summary>
    public sl_zigbee_zigbee_network_t zigbeeNetwork;

    /// <summary>
    /// Data associated with the ZLL security algorithm.
    /// </summary>
    public sl_zigbee_zll_security_algorithm_data_t securityAlgorithm;

    /// <summary>
    /// Associated EUI64.
    /// </summary>
    public sl_802154_long_addr_t eui64;

    /// <summary>
    /// The node id.
    /// </summary>
    public sl_802154_short_addr_t nodeId;

    /// <summary>
    /// The ZLL state.
    /// </summary>
    public sl_zigbee_zll_state_t state;

    /// <summary>
    /// The node type.
    /// </summary>
    public sl_zigbee_node_type_t nodeType;

    /// <summary>
    /// The number of sub devices.
    /// </summary>
    public byte numberSubDevices;

    /// <summary>
    /// The total number of group identifiers.
    /// </summary>
    public byte totalGroupIdentifiers;

    /// <summary>
    /// RSSI correction value.
    /// </summary>
    public byte rssiCorrection;

}

