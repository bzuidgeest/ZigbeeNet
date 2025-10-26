namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// A structure containing a key and its associated data.
/// </summary>
public struct sl_zigbee_key_struct_t
{
    /// <summary>
    /// A bitmask indicating the presence of data within the various fields in the structure.
    /// </summary>
    public sl_zigbee_key_struct_bitmask_t bitmask;

    /// <summary>
    /// The type of the key.
    /// </summary>
    public sl_zigbee_key_type_t type;

    /// <summary>
    /// The actual key data.
    /// </summary>
    public sl_zigbee_key_data_t key;

    /// <summary>
    /// The outgoing frame counter associated with the key.
    /// </summary>
    public uint outgoingFrameCounter;

    /// <summary>
    /// The frame counter of the partner device associated with the key.
    /// </summary>
    public uint incomingFrameCounter;

    /// <summary>
    /// The sequence number associated with the key.
    /// </summary>
    public byte sequenceNumber;

    /// <summary>
    /// The IEEE address of the partner device also in possession of the key.
    /// </summary>
    public sl_802154_long_addr_t partnerEUI64;

}

