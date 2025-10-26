namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Context for Zigbee Security Manager operations.
/// </summary>
public struct sl_zigbee_sec_man_context_t
{
    /// <summary>
    /// The type of key being referenced.
    /// </summary>
    public sl_zigbee_sec_man_key_type_t core_key_type;

    /// <summary>
    /// The index of the referenced key.
    /// </summary>
    public byte key_index;

    /// <summary>
    /// The type of key derivation operation to perform on a key.
    /// </summary>
    public sl_zigbee_sec_man_derived_key_type_t derived_type;

    /// <summary>
    /// The EUI64 associated with this key.
    /// </summary>
    public sl_802154_long_addr_t eui64;

    /// <summary>
    /// Multi-network index.
    /// </summary>
    public byte multi_network_index;

    /// <summary>
    /// Flag bitmask.
    /// </summary>
    public sl_zigbee_sec_man_flags_t flags;

    /// <summary>
    /// Algorithm to use with this key (for PSA APIs)
    /// </summary>
    public uint psa_key_alg_permission;

}

