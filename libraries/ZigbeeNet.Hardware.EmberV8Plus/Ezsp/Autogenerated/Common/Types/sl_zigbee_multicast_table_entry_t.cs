namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// A multicast table entry indicates that a particular endpoint is a member of a particular multicast group. Only devices with an endpoint in a multicast group will receive messages sent to that multicast group.
/// </summary>
public struct sl_zigbee_multicast_table_entry_t
{
    /// <summary>
    /// The multicast group ID.
    /// </summary>
    public sl_zigbee_multicast_id_t multicastId;

    /// <summary>
    /// The endpoint that is a member, or 0 if this entry is not in use (the ZDO is not a member of any multicast groups.)
    /// </summary>
    public byte endpoint;

    /// <summary>
    /// The network index of the network the entry is related to.
    /// </summary>
    public byte networkIndex;

}

