namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Identifies a policy decision.
/// </summary>
public enum ZigbeeEzspDecisionId : byte
{
	/// <summary>
	/// SL_ZIGBEE_EZSP_BINDING_MODIFICATION_POLICY default decision. Do not allow the local binding table to be changed by remote nodes.
	/// </summary>
    SL_ZIGBEE_EZSP_DISALLOW_BINDING_MODIFICATION = 0x10,
	/// <summary>
	/// SL_ZIGBEE_EZSP_BINDING_MODIFICATION_POLICY decision. Allow remote nodes to change the local binding table.
	/// </summary>
    SL_ZIGBEE_EZSP_ALLOW_BINDING_MODIFICATION = 0x11,
	/// <summary>
	/// SL_ZIGBEE_EZSP_BINDING_MODIFICATION_POLICY decision. Allows remote nodes to set local binding entries only if the entries correspond to endpoints defined on the device, and for output clusters bound to those endpoints.
	/// </summary>
    SL_ZIGBEE_EZSP_CHECK_BINDING_MODIFICATIONS_ARE_VALID_ENDPOINT_CLUSTERS = 0x12,
	/// <summary>
	/// SL_ZIGBEE_EZSP_UNICAST_REPLIES_POLICY default decision. The NCP will automatically send an empty reply (containing no payload) for every unicast received.
	/// </summary>
    SL_ZIGBEE_EZSP_HOST_WILL_NOT_SUPPLY_REPLY = 0x20,
	/// <summary>
	/// SL_ZIGBEE_EZSP_UNICAST_REPLIES_POLICY decision. The NCP will only send a reply if it receives a sendReply command from the Host.
	/// </summary>
    SL_ZIGBEE_EZSP_HOST_WILL_SUPPLY_REPLY = 0x21,
	/// <summary>
	/// SL_ZIGBEE_EZSP_POLL_HANDLER_POLICY default decision. Do not inform the Host when a child polls.
	/// </summary>
    SL_ZIGBEE_EZSP_POLL_HANDLER_IGNORE = 0x30,
	/// <summary>
	/// SL_ZIGBEE_EZSP_POLL_HANDLER_POLICY decision. Generate a pollHandler callback when a child polls.
	/// </summary>
    SL_ZIGBEE_EZSP_POLL_HANDLER_CALLBACK = 0x31,
	/// <summary>
	/// SL_ZIGBEE_EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY default decision. Include only the message tag in the messageSentHandler callback.
	/// </summary>
    SL_ZIGBEE_EZSP_MESSAGE_TAG_ONLY_IN_CALLBACK = 0x40,
	/// <summary>
	/// SL_ZIGBEE_EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY decision. Include both the message tag and the message contents in the messageSentHandler callback.
	/// </summary>
    SL_ZIGBEE_EZSP_MESSAGE_TAG_AND_CONTENTS_IN_CALLBACK = 0x41,
	/// <summary>
	/// SL_ZIGBEE_EZSP_TC_KEY_REQUEST_POLICY decision. When the Trust Center receives a request for a Trust Center link key, it will be ignored.
	/// </summary>
    SL_ZIGBEE_EZSP_DENY_TC_KEY_REQUESTS = 0x50,
	/// <summary>
	/// SL_ZIGBEE_EZSP_TC_KEY_REQUEST_POLICY decision. When the Trust Center receives a request for a Trust Center link key, it will reply to it with the corresponding key.
	/// </summary>
    SL_ZIGBEE_EZSP_ALLOW_TC_KEY_REQUESTS_AND_SEND_CURRENT_KEY = 0x51,
	/// <summary>
	/// SL_ZIGBEE_EZSP_TC_KEY_REQUEST_POLICY decision. When the Trust Center receives a request for a Trust Center link key, it will generate a key to send to the joiner. After generation, the key will be added to the transient key tabe and After verification, this key will be added into the link key table
	/// </summary>
    SL_ZIGBEE_EZSP_ALLOW_TC_KEY_REQUEST_AND_GENERATE_NEW_KEY = 0x52,
	/// <summary>
	/// SL_ZIGBEE_EZSP_APP_KEY_REQUEST_POLICY decision. When the Trust Center receives a request for an application link key, it will be ignored.
	/// </summary>
    SL_ZIGBEE_EZSP_DENY_APP_KEY_REQUESTS = 0x60,
	/// <summary>
	/// SL_ZIGBEE_EZSP_APP_KEY_REQUEST_POLICY decision. When the Trust Center receives a request for an application link key, it will randomly generate a key and send it to both partners.
	/// </summary>
    SL_ZIGBEE_EZSP_ALLOW_APP_KEY_REQUESTS = 0x61,
	/// <summary>
	/// Indicates that packet validate library checks are enabled on the NCP.
	/// </summary>
    SL_ZIGBEE_EZSP_PACKET_VALIDATE_LIBRARY_CHECKS_ENABLED = 0x62,
	/// <summary>
	/// Indicates that packet validate library checks are NOT enabled on the NCP.
	/// </summary>
    SL_ZIGBEE_EZSP_PACKET_VALIDATE_LIBRARY_CHECKS_DISABLED = 0x63
}
