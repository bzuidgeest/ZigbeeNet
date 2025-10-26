namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The public key data used in CBKE.
/// </summary>
public struct sl_zigbee_public_key_data_t
{
    /// <summary>
    /// The public key data.
    /// </summary>
    public fixed byte contents[22];

}

