namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// The implicit certificate used in CBKE.
/// </summary>
public struct sl_zigbee_certificate_data_t
{
    /// <summary>
    /// The certificate data.
    /// </summary>
    public fixed byte contents[48];

}

