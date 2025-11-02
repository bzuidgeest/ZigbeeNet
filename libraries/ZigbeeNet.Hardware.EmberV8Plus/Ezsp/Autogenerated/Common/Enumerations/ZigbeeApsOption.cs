namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Options to use when sending a message.
/// </summary>
public enum ZigbeeApsOption : ushort
{
	/// <summary>
	/// No options.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_NONE = 0x0000,
	/// <summary>
	/// Send the message using APS Encryption, using the Link Key shared with the destination node to encrypt the data at the APS Level.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_ENCRYPTION = 0x0020,
	/// <summary>
	/// Resend the message using the APS retry mechanism.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_RETRY = 0x0040,
	/// <summary>
	/// Causes a route discovery to be initiated if no route to the destination is known.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_ENABLE_ROUTE_DISCOVERY = 0x0100,
	/// <summary>
	/// Causes a route discovery to be initiated even if one is known.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_FORCE_ROUTE_DISCOVERY = 0x0200,
	/// <summary>
	/// Include the source EUI64 in the network frame.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_SOURCE_EUI64 = 0x0400,
	/// <summary>
	/// Include the destination EUI64 in the network frame.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_DESTINATION_EUI64 = 0x0800,
	/// <summary>
	/// Send a ZDO request to discover the node ID of the destination, if it is not already know.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_ENABLE_ADDRESS_DISCOVERY = 0x1000,
	/// <summary>
	/// Reserved.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_POLL_RESPONSE = 0x2000,
	/// <summary>
	/// This incoming message is a ZDO request not handled by the EmberZNet stack, and the application is responsible for sending a ZDO response. This flag is used only when the ZDO is configured to have requests handled by the application. See the EZSP_CONFIG_APPLICATION_ZDO_CONFIGURATION configuration parameter for more information.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_ZDO_RESPONSE_REQUIRED = 0x4000,
	/// <summary>
	/// This message is part of a fragmented message. This option may only be set for unicasts. The groupId field gives the index of this fragment in the low-order byte. If the low-order byte is zero this is the first fragment and the high-order byte contains the number of fragments in the message.
	/// </summary>
    SL_ZIGBEE_APS_OPTION_FRAGMENT = 0x8000
}
