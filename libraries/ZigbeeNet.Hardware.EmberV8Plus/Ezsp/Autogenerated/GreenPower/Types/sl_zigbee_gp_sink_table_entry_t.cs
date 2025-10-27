namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

/// <summary>
/// The internal representation of a sink table entry.
/// </summary>
public struct sl_zigbee_gp_sink_table_entry_t
{
    /// <summary>
    /// Internal status of the sink table entry.
    /// </summary>
    public sl_zigbee_gp_sink_table_entry_status_t status;

    /// <summary>
    /// The tunneling options (this contains both options and extendedOptions from the spec).
    /// </summary>
    public uint options;

    /// <summary>
    /// The addressing info of the GPD.
    /// </summary>
    public sl_zigbee_gp_address_t gpd;

    /// <summary>
    /// The device id for the GPD.
    /// </summary>
    public byte deviceId;

    /// <summary>
    /// The list of sinks (hardcoded to 2 which is the spec minimum).
    /// </summary>
    // Array field with symbolic size: GP_SINK_LIST_ENTRIES
    // public fixed sl_zigbee_gp_sink_list_entry_t sinkList[GP_SINK_LIST_ENTRIES];

    /// <summary>
    /// The assigned alias for the GPD.
    /// </summary>
    public sl_802154_short_addr_t assignedAlias;

    /// <summary>
    /// The groupcast radius.
    /// </summary>
    public byte groupcastRadius;

    /// <summary>
    /// The security options field.
    /// </summary>
    public byte securityOptions;

    /// <summary>
    /// The security frame counter of the GPD.
    /// </summary>
    public sl_zigbee_gp_security_frame_counter_t gpdSecurityFrameCounter;

    /// <summary>
    /// The key to use for GPD.
    /// </summary>
    public sl_zigbee_key_data_t gpdKey;

}

