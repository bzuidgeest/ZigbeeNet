namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Metadata for APS link keys.
/// </summary>
public struct sl_zigbee_sec_man_aps_key_metadata_t
{
    /// <summary>
    /// Bitmask of key properties
    /// </summary>
    public sl_zigbee_key_struct_bitmask_t bitmask;

    /// <summary>
    /// Outgoing frame counter.
    /// </summary>
    public uint outgoing_frame_counter;

    /// <summary>
    /// Incoming frame counter.
    /// </summary>
    public uint incoming_frame_counter;

    /// <summary>
    /// Remaining lifetime (for transient keys).
    /// </summary>
    public ushort ttl_in_seconds;

}

