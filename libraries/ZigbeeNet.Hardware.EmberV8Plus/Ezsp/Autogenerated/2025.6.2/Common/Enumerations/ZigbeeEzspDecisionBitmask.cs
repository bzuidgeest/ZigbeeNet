#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// This is the policy decision bitmask that controls the trust center decision strategies. The bitmask is modified and extracted from the sl_zigbee_ezsp_decision_id_t for supporting bitmask operations.
/// </summary>
public enum ZigbeeEzspDecisionBitmask : ushort
{
	/// <summary>
	/// Disallow joins and rejoins.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_BITMASK_DEFAULT_CONFIGURATION = 0x0000,
	/// <summary>
	/// Send the network key to all joining devices.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_ALLOW_JOINS = 0x0001,
	/// <summary>
	/// Send the network key to all rejoining devices.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_ALLOW_UNSECURED_REJOINS = 0x0002,
	/// <summary>
	/// Send the network key in the clear.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_SEND_KEY_IN_CLEAR = 0x0004,
	/// <summary>
	/// Do nothing for unsecured rejoins.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_IGNORE_UNSECURED_REJOINS = 0x0008,
	/// <summary>
	/// Allow joins if there is an entry in the transient key table.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_JOINS_USE_INSTALL_CODE_KEY = 0x0010,
	/// <summary>
	/// Delay sending the network key to a new joining device.
	/// </summary>
		SL_ZIGBEE_EZSP_DECISION_DEFER_JOINS = 0x0020
}

#endif