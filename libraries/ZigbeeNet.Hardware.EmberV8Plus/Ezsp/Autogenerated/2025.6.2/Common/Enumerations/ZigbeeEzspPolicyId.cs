#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Identifies a policy.
/// </summary>
public enum ZigbeeEzspPolicyId : byte
{
	/// <summary>
	/// Controls trust center behavior.
	/// </summary>
	SL_ZIGBEE_EZSP_TRUST_CENTER_POLICY = 0x00,
	/// <summary>
	/// Controls how external binding modification requests are handled.
	/// </summary>
	SL_ZIGBEE_EZSP_BINDING_MODIFICATION_POLICY = 0x01,
	/// <summary>
	/// Controls whether the Host supplies unicast replies.
	/// </summary>
	SL_ZIGBEE_EZSP_UNICAST_REPLIES_POLICY = 0x02,
	/// <summary>
	/// Controls whether pollHandler callbacks are generated.
	/// </summary>
	SL_ZIGBEE_EZSP_POLL_HANDLER_POLICY = 0x03,
	/// <summary>
	/// Controls whether the message contents are included in the messageSentHandler callback.
	/// </summary>
	SL_ZIGBEE_EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY = 0x04,
	/// <summary>
	/// Controls whether the Trust Center will respond to Trust Center link key requests.
	/// </summary>
	SL_ZIGBEE_EZSP_TC_KEY_REQUEST_POLICY = 0x05,
	/// <summary>
	/// Controls whether the Trust Center will respond to application link key requests.
	/// </summary>
	SL_ZIGBEE_EZSP_APP_KEY_REQUEST_POLICY = 0x06,
	/// <summary>
	/// Controls whether ZigBee packets that appear invalid are automatically dropped by the stack. A counter will be incremented when this occurs.
	/// </summary>
	SL_ZIGBEE_EZSP_PACKET_VALIDATE_LIBRARY_POLICY = 0x07,
	/// <summary>
	/// Controls whether the stack will process ZLL messages.
	/// </summary>
	SL_ZIGBEE_EZSP_ZLL_POLICY = 0x08,
	/// <summary>
	/// Controls whether Trust Center (insecure) rejoins for devices using the well-known link key are accepted. If rejoining using the well-known key is allowed, it is disabled again after sli_zigbee_allow_tc_rejoins_using_well_known_key_timeout_sec seconds.
	/// </summary>
	SL_ZIGBEE_EZSP_TC_REJOINS_USING_WELL_KNOWN_KEY_POLICY = 0x09
}

#endif