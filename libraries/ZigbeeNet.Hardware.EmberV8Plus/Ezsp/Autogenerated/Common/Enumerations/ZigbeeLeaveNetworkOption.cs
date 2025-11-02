namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Use in case zigbee leave network with options
/// </summary>
public enum ZigbeeLeaveNetworkOption : byte
{
	/// <summary>
	/// Leave with no option.
	/// </summary>
    SL_ZIGBEE_LEAVE_NWK_WITH_NO_OPTION = 0x00,
	/// <summary>
	/// Leave with option rejoin.
	/// </summary>
    SL_ZIGBEE_LEAVE_NWK_WITH_OPTION_REJOIN = 0x20,
	/// <summary>
	/// Leave is requested.
	/// </summary>
    SL_ZIGBEE_LEAVE_NWK_IS_REQUESTED = 0x40
}
