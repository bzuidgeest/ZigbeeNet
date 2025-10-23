// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspDecisionId enumeration
    /// </summary>
    public enum EzspDecisionId
    {
        EZSP_DISALLOW_BINDING_MODIFICATION = 0x10,
        /// <summary>
        /// binding table to be changed by remote nodes.
        /// </summary>
        EZSP_ALLOW_BINDING_MODIFICATION = 0x11,
        /// <summary>
        /// local binding table.
        /// </summary>
        EZSP_CHECK_BINDING_MODIFICATIONS_ARE_VALID_ENDPOINT_CLUSTERS = 0x12,
        /// <summary>
        /// device, and for output clusters bound to those endpoints.
        /// </summary>
        EZSP_HOST_WILL_NOT_SUPPLY_REPLY = 0x20,
        /// <summary>
        /// send an empty reply (containing no payload) for every unicast received.
        /// </summary>
        EZSP_HOST_WILL_SUPPLY_REPLY = 0x21,
        /// <summary>
        /// receives a sendReply command from the Host.
        /// </summary>
        EZSP_POLL_HANDLER_IGNORE = 0x30,
        /// <summary>
        /// child polls.
        /// </summary>
        EZSP_POLL_HANDLER_CALLBACK = 0x31,
        /// <summary>
        /// child polls.
        /// </summary>
        EZSP_MESSAGE_TAG_ONLY_IN_CALLBACK = 0x40,
        /// <summary>
        /// message tag in the messageSentHandler callback.
        /// </summary>
        EZSP_MESSAGE_TAG_AND_CONTENTS_IN_CALLBACK = 0x41,
        /// <summary>
        /// tag and the message contents in the messageSentHandler callback.
        /// </summary>
        EZSP_DENY_TC_KEY_REQUESTS = 0x50,
        /// <summary>
        /// request for a Trust Center link key, it will be ignored.
        /// </summary>
        EZSP_ALLOW_TC_KEY_REQUESTS_AND_SEND_CURRENT_KEY = 0x51,
        /// <summary>
        /// corresponding key.
        /// </summary>
        EZSP_ALLOW_TC_KEY_REQUEST_AND_GENERATE_NEW_KEY = 0x52,
        /// <summary>
        /// and After verification, this key will be added into the link key table
        /// </summary>
        EZSP_DENY_APP_KEY_REQUESTS = 0x60,
        /// <summary>
        /// request for an application link key, it will be ignored.
        /// </summary>
        EZSP_ALLOW_APP_KEY_REQUESTS = 0x61,
        /// <summary>
        /// send it to both partners.
        /// </summary>
        EZSP_PACKET_VALIDATE_LIBRARY_CHECKS_ENABLED = 0x62,
        /// <summary>
        /// Indicates that packet validate library checks are enabled on the NCP.
        /// </summary>
        EZSP_PACKET_VALIDATE_LIBRARY_CHECKS_DISABLED = 0x63
    }
}
