#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// ZLL key encryption algorithm enumeration.
/// </summary>
public enum ZigbeeZllKeyIndex : byte
{
	/// <summary>
	/// Key encryption algorithm for use during development.
	/// </summary>
	SL_ZIGBEE_ZLL_KEY_INDEX_DEVELOPMENT = 0x00,
	/// <summary>
	/// Key encryption algorithm shared by all certified devices.
	/// </summary>
	SL_ZIGBEE_ZLL_KEY_INDEX_MASTER = 0x04,
	/// <summary>
	/// Key encryption algorithm for use during development and certification.
	/// </summary>
	SL_ZIGBEE_ZLL_KEY_INDEX_CERTIFICATION = 0x0F
}

#endif