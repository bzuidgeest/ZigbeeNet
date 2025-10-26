namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The transient key data structure.
/// </summary>
public struct sl_zigbee_transient_key_data_t
{
    /// <summary>
    /// The IEEE address paired with the transient link key.
    /// </summary>
    public sl_802154_long_addr_t eui64;

    /// <summary>
    /// The key data structure matching the transient key.
    /// </summary>
    public sl_zigbee_key_data_t keyData;

    /// <summary>
    /// The incoming frame counter associated with this key.
    /// </summary>
    public uint incomingFrameCounter;

    /// <summary>
    /// This bitmask indicates whether various fields in the structure contain valid data.
    /// </summary>
    public sl_zigbee_key_struct_bitmask_t bitmask;

    /// <summary>
    /// The number of seconds remaining before the key is automatically timed out of the transient key table.
    /// </summary>
    public ushort remainingTimeSeconds;

    /// <summary>
    /// The network index indicates which NWK uses this key.
    /// </summary>
    public byte networkIndex;

}

