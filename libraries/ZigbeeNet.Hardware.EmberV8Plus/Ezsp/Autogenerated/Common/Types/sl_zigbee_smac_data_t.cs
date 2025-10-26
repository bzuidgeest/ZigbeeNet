namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// The Shared Message Authentication Code data used in CBKE.
/// </summary>
public struct sl_zigbee_smac_data_t
{
    /// <summary>
    /// The Shared Message Authentication Code data.
    /// </summary>
    public fixed byte contents[16];

}

