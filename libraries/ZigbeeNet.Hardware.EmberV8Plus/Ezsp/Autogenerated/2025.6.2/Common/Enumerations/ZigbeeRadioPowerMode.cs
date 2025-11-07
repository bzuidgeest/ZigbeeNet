#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Radio power modes.
/// </summary>
public enum ZigbeeRadioPowerMode : byte
{
	/// <summary>
	/// The radio receiver is switched on.
	/// </summary>
    SL_ZIGBEE_RADIO_POWER_MODE_RX_ON = 0,
	/// <summary>
	/// The radio receiver is switched off.
	/// </summary>
    SL_ZIGBEE_RADIO_POWER_MODE_OFF = 1
}

#endif