#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Describes the presence of valid data within the sl_zigbee_key_struct_t structure.
/// </summary>
public enum ZigbeeKeyStructBitmask : ushort
{
	/// <summary>
	/// The key has a sequence number associated with it.
	/// </summary>
	SL_ZIGBEE_KEY_HAS_SEQUENCE_NUMBER = 0x0001,
	/// <summary>
	/// The key has an outgoing frame counter associated with it.
	/// </summary>
	SL_ZIGBEE_KEY_HAS_OUTGOING_FRAME_COUNTER = 0x0002,
	/// <summary>
	/// The key has an incoming frame counter associated with it.
	/// </summary>
	SL_ZIGBEE_KEY_HAS_INCOMING_FRAME_COUNTER = 0x0004,
	/// <summary>
	/// The key has a Partner IEEE address associated with it.
	/// </summary>
	SL_ZIGBEE_KEY_HAS_PARTNER_EUI64 = 0x0008
}

#endif