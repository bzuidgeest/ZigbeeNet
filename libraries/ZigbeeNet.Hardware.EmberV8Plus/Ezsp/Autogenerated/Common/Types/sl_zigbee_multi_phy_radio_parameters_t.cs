namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Radio parameters.
/// </summary>
public struct sl_zigbee_multi_phy_radio_parameters_t
{
    /// <summary>
    /// A power setting, in dBm.
    /// </summary>
    public sbyte radioTxPower;

    /// <summary>
    /// A radio page.
    /// </summary>
    public byte radioPage;

    /// <summary>
    /// A radio channel.
    /// </summary>
    public byte radioChannel;

}

