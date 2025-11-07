#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Key types recognized by Zigbee Security Manager.
/// </summary>
public enum ZigbeeSecManKeyType : byte
{
	/// <summary>
	/// No key type.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_NONE = 0,
	/// <summary>
	/// Network Key (either current or alternate).
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_NETWORK = 1,
	/// <summary>
	/// Preconfigured Trust Center Link Key.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_TC_LINK = 2,
	/// <summary>
	/// Transient key.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_TC_LINK_WITH_TIMEOUT = 3,
	/// <summary>
	/// Link key in table.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_APP_LINK = 4,
	/// <summary>
	/// Encryption key in ZLL.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_ZLL_ENCRYPTION_KEY = 6,
	/// <summary>
	/// Preconfigured key in ZLL.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_ZLL_PRECONFIGURED_KEY = 7,
	/// <summary>
	/// GP Proxy table key.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_GREEN_POWER_PROXY_TABLE_KEY = 8,
	/// <summary>
	/// GP Sink table key.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_GREEN_POWER_SINK_TABLE_KEY = 9,
	/// <summary>
	/// Generic key type available to use for crypto operations.
	/// </summary>
    SL_ZB_SEC_MAN_KEY_TYPE_INTERNAL = 10
}

#endif