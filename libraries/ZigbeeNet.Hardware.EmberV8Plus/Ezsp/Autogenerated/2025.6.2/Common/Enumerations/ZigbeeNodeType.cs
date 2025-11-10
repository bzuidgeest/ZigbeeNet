#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// The type of the node.
/// </summary>
public enum ZigbeeNodeType : byte
{
	/// <summary>
	/// Device is not joined.
	/// </summary>
	SL_ZIGBEE_UNKNOWN_DEVICE = 0x00,
	/// <summary>
	/// Device type has not changed since last join.
	/// </summary>
	SL_ZIGBEE_DEVICE_TYPE_UNCHANGED = 0x00,
	/// <summary>
	/// Will relay messages and can act as a parent to other nodes.
	/// </summary>
	SL_ZIGBEE_COORDINATOR = 0x01,
	/// <summary>
	/// Will relay messages and can act as a parent to other nodes.
	/// </summary>
	SL_ZIGBEE_ROUTER = 0x02,
	/// <summary>
	/// Communicates only with its parent and will not relay messages.
	/// </summary>
	SL_ZIGBEE_END_DEVICE = 0x03,
	/// <summary>
	/// An end device whose radio can be turned off to save power. The application must poll to receive messages.
	/// </summary>
	SL_ZIGBEE_SLEEPY_END_DEVICE = 0x04
}

#endif