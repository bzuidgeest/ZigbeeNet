// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspPolicyId enumeration
    /// </summary>
    public enum EzspPolicyId
    {
        EZSP_TRUST_CENTER_POLICY = 0x00,
        /// <summary>
        /// Controls trust center behavior.
        /// </summary>
        EZSP_BINDING_MODIFICATION_POLICY = 0x01,
        /// <summary>
        /// Controls how external binding modification requests are handled.
        /// </summary>
        EZSP_UNICAST_REPLIES_POLICY = 0x02,
        /// <summary>
        /// Controls whether the Host supplies unicast replies.
        /// </summary>
        EZSP_POLL_HANDLER_POLICY = 0x03,
        /// <summary>
        /// Controls whether pollHandler callbacks are generated.
        /// </summary>
        EZSP_MESSAGE_CONTENTS_IN_CALLBACK_POLICY = 0x04,
        /// <summary>
        /// messageSentHandler callback.
        /// </summary>
        EZSP_TC_KEY_REQUEST_POLICY = 0x05,
        /// <summary>
        /// requests.
        /// </summary>
        EZSP_APP_KEY_REQUEST_POLICY = 0x06,
        /// <summary>
        /// requests.
        /// </summary>
        EZSP_PACKET_VALIDATE_LIBRARY_POLICY = 0x07,
        /// <summary>
        /// dropped by the stack. A counter will be incremented when this occurs.
        /// </summary>
        EZSP_ZLL_POLICY = 0x08,
        /// <summary>
        /// Controls whether the stack will process ZLL messages.
        /// </summary>
        EZSP_TC_REJOINS_USING_WELL_KNOWN_KEY_POLICY = 0x09
    }
}
