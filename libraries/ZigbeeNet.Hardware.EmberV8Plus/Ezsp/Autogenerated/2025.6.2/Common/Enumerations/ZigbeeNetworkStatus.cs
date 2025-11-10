#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// The possible join states for a node.
/// </summary>
public enum ZigbeeNetworkStatus : byte
{
	/// <summary>
	/// The node is not associated with a network in any way.
	/// </summary>
		SL_ZIGBEE_NO_NETWORK = 0x00,
	/// <summary>
	/// The node is currently attempting to join a network.
	/// </summary>
		SL_ZIGBEE_JOINING_NETWORK = 0x01,
	/// <summary>
	/// The node is joined to a network.
	/// </summary>
		SL_ZIGBEE_JOINED_NETWORK = 0x02,
	/// <summary>
	/// The node is an end device joined to a network but its parent is not responding.
	/// </summary>
		SL_ZIGBEE_JOINED_NETWORK_NO_PARENT = 0x03,
	/// <summary>
	/// The node is in the process of leaving its current network.
	/// </summary>
		SL_ZIGBEE_LEAVING_NETWORK = 0x04
}

#endif