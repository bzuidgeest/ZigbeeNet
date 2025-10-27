namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

/// <summary>
/// A sink list entry.
/// </summary>
public struct sl_zigbee_gp_sink_list_entry_t
{
    /// <summary>
    /// The sink list type.
    /// </summary>
    public byte type;

    /// <summary>
    /// The EUI64 of the target sink.
    /// </summary>
    public sl_802154_long_addr_t sinkEUI;

    /// <summary>
    /// The short address of the target sink.
    /// </summary>
    public sl_802154_short_addr_t sinkNodeId;

}

