#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// See enumeration in gp-types.h
/// </summary>
public enum ZigbeeGpStatus : byte
{
	/// <summary>
	/// Success Status
	/// </summary>
    SL_ZIGBEE_GP_STATUS_OK,
	/// <summary>
	/// Match Frame
	/// </summary>
    SL_ZIGBEE_GP_STATUS_MATCH,
	/// <summary>
	/// Drop Frame
	/// </summary>
    SL_ZIGBEE_GP_STATUS_DROP_FRAME,
	/// <summary>
	/// Frame Unprocessed
	/// </summary>
    SL_ZIGBEE_GP_STATUS_UNPROCESSED,
	/// <summary>
	/// Frame Pass Unprocessed
	/// </summary>
    SL_ZIGBEE_GP_STATUS_PASS_UNPROCESSED,
	/// <summary>
	/// Frame TX Then Drop
	/// </summary>
    SL_ZIGBEE_GP_STATUS_TX_THEN_DROP,
	/// <summary>
	/// No Security
	/// </summary>
    SL_ZIGBEE_GP_STATUS_NO_SECURITY,
	/// <summary>
	/// Security Failure
	/// </summary>
    SL_ZIGBEE_GP_STATUS_AUTH_FAILURE
}

#endif