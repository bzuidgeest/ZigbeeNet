#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Flags for key operations.
/// </summary>
public enum ZigbeeSecManFlags : byte
{
	/// <summary>
	/// No flags on operation.
	/// </summary>
	ZB_SEC_MAN_FLAG_NONE = 0,
	/// <summary>
	/// Context has a valid key index.
	/// </summary>
	ZB_SEC_MAN_FLAG_KEY_INDEX_IS_VALID = 1,
	/// <summary>
	/// Context has a valid EUI64.
	/// </summary>
	ZB_SEC_MAN_FLAG_EUI_IS_VALID = 2,
	/// <summary>
	/// Transient key being added hasn&apos;t yet been verified.
	/// </summary>
	ZB_SEC_MAN_FLAG_UNCONFIRMED_TRANSIENT_KEY = 4
}

#endif