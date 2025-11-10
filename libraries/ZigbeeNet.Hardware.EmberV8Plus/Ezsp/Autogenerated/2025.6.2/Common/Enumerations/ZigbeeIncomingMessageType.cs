#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Incoming message types.
/// </summary>
public enum ZigbeeIncomingMessageType : byte
{
	/// <summary>
	/// Unicast.
	/// </summary>
		SL_ZIGBEE_INCOMING_UNICAST = 0x00,
	/// <summary>
	/// Unicast reply.
	/// </summary>
		SL_ZIGBEE_INCOMING_UNICAST_REPLY = 0x01,
	/// <summary>
	/// Multicast.
	/// </summary>
		SL_ZIGBEE_INCOMING_MULTICAST = 0x02,
	/// <summary>
	/// Multicast sent by the local device.
	/// </summary>
		SL_ZIGBEE_INCOMING_MULTICAST_LOOPBACK = 0x03,
	/// <summary>
	/// Broadcast.
	/// </summary>
		SL_ZIGBEE_INCOMING_BROADCAST = 0x04,
	/// <summary>
	/// Broadcast sent by the local device.
	/// </summary>
		SL_ZIGBEE_INCOMING_BROADCAST_LOOPBACK = 0x05,
	/// <summary>
	/// Many to one route request.
	/// </summary>
		EMBER_INCOMING_MANY_TO_ONE_ROUTE_REQUEST = 0x06
}

#endif