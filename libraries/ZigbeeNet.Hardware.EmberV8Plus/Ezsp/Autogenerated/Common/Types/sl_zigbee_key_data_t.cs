namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// A 128-bit key.
/// </summary>
public struct sl_zigbee_key_data_t
{
    /// <summary>
    /// The key data.
    /// </summary>
    public fixed byte contents[16];

}

