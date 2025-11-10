#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Binding types.
/// </summary>
public enum ZigbeeBindingType : byte
{
	/// <summary>
	/// A binding that is currently not in use.
	/// </summary>
		SL_ZIGBEE_UNUSED_BINDING = 0x00,
	/// <summary>
	/// A unicast binding whose 64-bit identifier is the destination EUI64.
	/// </summary>
		SL_ZIGBEE_UNICAST_BINDING = 0x01,
	/// <summary>
	/// A unicast binding whose 64-bit identifier is the aggregator EUI64.
	/// </summary>
		SL_ZIGBEE_MANY_TO_ONE_BINDING = 0x02,
	/// <summary>
	/// A multicast binding whose 64-bit identifier is the group address. A multicast binding can be used to send messages to the group and to receive messages sent to the group.
	/// </summary>
		SL_ZIGBEE_MULTICAST_BINDING = 0x03
}

#endif