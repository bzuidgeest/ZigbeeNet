namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// ZLL address assignment data.
/// </summary>
public struct sl_zigbee_zll_address_assignment_t
{
    /// <summary>
    /// Relevant node id.
    /// </summary>
    public sl_802154_short_addr_t nodeId;

    /// <summary>
    /// Minimum free node id.
    /// </summary>
    public sl_802154_short_addr_t freeNodeIdMin;

    /// <summary>
    /// Maximum free node id.
    /// </summary>
    public sl_802154_short_addr_t freeNodeIdMax;

    /// <summary>
    /// Minimum group id.
    /// </summary>
    public sl_zigbee_multicast_id_t groupIdMin;

    /// <summary>
    /// Maximum group id.
    /// </summary>
    public sl_zigbee_multicast_id_t groupIdMax;

    /// <summary>
    /// Minimum free group id.
    /// </summary>
    public sl_zigbee_multicast_id_t freeGroupIdMin;

    /// <summary>
    /// Maximum free group id.
    /// </summary>
    public sl_zigbee_multicast_id_t freeGroupIdMax;

}

