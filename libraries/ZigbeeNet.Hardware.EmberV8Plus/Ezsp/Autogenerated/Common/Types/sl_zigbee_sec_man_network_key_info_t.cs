namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Metadata for network keys.
/// </summary>
public struct sl_zigbee_sec_man_network_key_info_t
{
    /// <summary>
    /// Whether the current network key is set.
    /// </summary>
    public bool network_key_set;

    /// <summary>
    /// Whether the alternate network key is set.
    /// </summary>
    public bool alternate_network_key_set;

    /// <summary>
    /// Current network key sequence number.
    /// </summary>
    public byte network_key_sequence_number;

    /// <summary>
    /// Alternate network key sequence number.
    /// </summary>
    public byte alt_network_key_sequence_number;

    /// <summary>
    /// Frame counter for the network key.
    /// </summary>
    public uint network_key_frame_counter;

}

