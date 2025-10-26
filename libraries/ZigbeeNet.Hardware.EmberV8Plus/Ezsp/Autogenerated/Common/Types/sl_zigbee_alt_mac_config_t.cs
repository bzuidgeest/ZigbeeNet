namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Defines alternate MAC configuration parameters.
/// </summary>
public struct sl_zigbee_alt_mac_config_t
{
    /// <summary>
    /// Scan duration over alternate MAC.
    /// </summary>
    public ushort scanDuration;

    /// <summary>
    /// To register the transmit callback. Called when there is packet to transmit.
    /// </summary>
    public MacTransmitCallback macTransmit;

}

