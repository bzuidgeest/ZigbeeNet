namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// A 128-bit key.
/// </summary>
public struct sl_zigbee_sec_man_key_t
{
    /// <summary>
    /// The key data.
    /// </summary>
    public fixed byte key[16];

}

