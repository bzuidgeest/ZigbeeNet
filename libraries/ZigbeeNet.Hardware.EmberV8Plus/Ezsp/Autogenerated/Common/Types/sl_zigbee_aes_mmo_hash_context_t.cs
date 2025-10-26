namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// The hash context for an ongoing hash operation.
/// </summary>
public struct sl_zigbee_aes_mmo_hash_context_t
{
    /// <summary>
    /// The result of ongoing the hash operation.
    /// </summary>
    public fixed byte result[16];

    /// <summary>
    /// The total length of the data that has been hashed so far.
    /// </summary>
    public uint length;

}

