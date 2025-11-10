#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// ZLL device state identifier
/// </summary>
public enum ZigbeeZllState : ushort
{
	/// <summary>
	/// No state.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_NONE = 0x0000,
	/// <summary>
	/// The device is factory new.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_FACTORY_NEW = 0x0001,
	/// <summary>
	/// The device is capable of assigning addresses to other devices.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_ADDRESS_ASSIGNMENT_CAPABLE = 0x0002,
	/// <summary>
	/// The device is initiating a link operation.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_LINK_INITIATOR = 0x0010,
	/// <summary>
	/// The device is requesting link priority.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_LINK_PRIORITY_REQUEST = 0x0020,
	/// <summary>
	/// The device is on a non-ZLL network.
	/// </summary>
	SL_ZIGBEE_ZLL_STATE_NON_ZLL_NETWORK = 0x0100
}

#endif