namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The private key data used in CBKE.
/// </summary>
public struct sl_zigbee_private_key_data_t
{
    /// <summary>
    /// The private key data.
    /// </summary>
    public fixed byte contents[21];

}

