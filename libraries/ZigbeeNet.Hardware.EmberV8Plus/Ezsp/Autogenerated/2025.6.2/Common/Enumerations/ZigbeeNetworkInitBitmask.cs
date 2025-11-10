#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Bitmask options for sli_zigbee_stack_network_init()
/// </summary>
public enum ZigbeeNetworkInitBitmask : ushort
{
	/// <summary>
	/// No options for Network Init
	/// </summary>
		SL_ZIGBEE_NETWORK_INIT_NO_OPTIONS = 0x0000,
	/// <summary>
	/// Save parent info (node ID and EUI64) in a token during joining/rejoin, and restore on reboot.
	/// </summary>
		SL_ZIGBEE_NETWORK_INIT_PARENT_INFO_IN_TOKEN = 0x0001,
	/// <summary>
	/// Send a rejoin request as an end device on reboot if parent information is persisted.
	/// </summary>
		SL_ZIGBEE_NETWORK_INIT_END_DEVICE_REJOIN_ON_REBOOT = 0x0002
}

#endif