#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Values for SL_ZIGBEE_EZSP_CONFIG_TX_POWER_MODE.
/// </summary>
public enum Emberconfigtxpowermode : ushort
{
	/// <summary>
	/// Normal power mode and bi-directional RF transmitter output.
	/// </summary>
	SL_ZIGBEE_TX_POWER_MODE_DEFAULT = 0x00,
	/// <summary>
	/// Enable boost power mode. This is a high-performance radio mode which offers increased receive sensitivity and transmit power at the cost of an increase in power consumption.
	/// </summary>
	SL_ZIGBEE_TX_POWER_MODE_BOOST = 0x01,
	/// <summary>
	/// Enable the alternate transmitter output. This allows for simplified connection to an external power amplifier via the RF_TX_ALT_P and RF_TX_ALT_N pins.
	/// </summary>
	SL_ZIGBEE_TX_POWER_MODE_ALTERNATE = 0x02,
	/// <summary>
	/// Enable both boost mode and the alternate transmitter output.
	/// </summary>
	SL_ZIGBEE_TX_POWER_MODE_BOOST_AND_ALTERNATE = 0x03
}

#endif