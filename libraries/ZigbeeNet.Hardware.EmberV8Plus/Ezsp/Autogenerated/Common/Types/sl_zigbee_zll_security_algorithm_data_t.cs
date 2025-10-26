namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Data associated with the ZLL security algorithm.
/// </summary>
public struct sl_zigbee_zll_security_algorithm_data_t
{
    /// <summary>
    /// Transaction identifier.
    /// </summary>
    public uint transactionId;

    /// <summary>
    /// Response identifier.
    /// </summary>
    public uint responseId;

    /// <summary>
    /// Bitmask.
    /// </summary>
    public ushort bitmask;

}

