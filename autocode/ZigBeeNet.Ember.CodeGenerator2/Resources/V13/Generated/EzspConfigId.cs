// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspConfigId enumeration
    /// </summary>
    public enum EzspConfigId
    {
        EZSP_CONFIG_PACKET_BUFFER_COUNT = 0x01,
        /// <summary>
        /// EMBER_PACKET_BUFFER_COUNT macro when building the NCP project.
        /// </summary>
        EZSP_CONFIG_NEIGHBOR_TABLE_SIZE = 0x02,
        /// <summary>
        /// neighbor is a node within radio range.
        /// </summary>
        EZSP_CONFIG_APS_UNICAST_MESSAGE_COUNT = 0x03,
        /// <summary>
        /// any time.
        /// </summary>
        EZSP_CONFIG_BINDING_TABLE_SIZE = 0x04,
        /// <summary>
        /// The maximum number of non-volatile bindings supported by the stack.
        /// </summary>
        EZSP_CONFIG_ADDRESS_TABLE_SIZE = 0x05,
        /// <summary>
        /// and the value of ::EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE.
        /// </summary>
        EZSP_CONFIG_MULTICAST_TABLE_SIZE = 0x06,
        /// <summary>
        /// The maximum number of multicast groups that the device may be a member of.
        /// </summary>
        EZSP_CONFIG_ROUTE_TABLE_SIZE = 0x07,
        /// <summary>
        /// others.
        /// </summary>
        EZSP_CONFIG_DISCOVERY_TABLE_SIZE = 0x08,
        /// <summary>
        /// The number of simultaneous route discoveries that a node will support.
        /// </summary>
        EZSP_CONFIG_STACK_PROFILE = 0x0C,
        /// <summary>
        /// Specifies the stack profile.
        /// </summary>
        EZSP_CONFIG_SECURITY_LEVEL = 0x0D,
        /// <summary>
        /// four-byte MIC is used for authentication).
        /// </summary>
        EZSP_CONFIG_MAX_HOPS = 0x10,
        /// <summary>
        /// The maximum number of hops for a message.
        /// </summary>
        EZSP_CONFIG_MAX_END_DEVICE_CHILDREN = 0x11,
        /// <summary>
        /// The maximum number of end device children that a router will support.
        /// </summary>
        EZSP_CONFIG_INDIRECT_TRANSMISSION_TIMEOUT = 0x12,
        /// <summary>
        /// transmission to a child.
        /// </summary>
        EZSP_CONFIG_END_DEVICE_POLL_TIMEOUT = 0x13,
        /// <summary>
        /// is 2^N minutes, ranging from 2^1 = 2 minutes to 2^14 = 16384 minutes.
        /// </summary>
        EZSP_CONFIG_TX_POWER_MODE = 0x17,
        /// <summary>
        /// Enables boost power mode and/or the alternate transmitter output.
        /// </summary>
        EZSP_CONFIG_DISABLE_RELAY = 0x18,
        /// <summary>
        /// messages.
        /// </summary>
        EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE = 0x19,
        /// <summary>
        /// setting and the value of ::EZSP_CONFIG_ADDRESS_TABLE_SIZE.)
        /// </summary>
        EZSP_CONFIG_SOURCE_ROUTE_TABLE_SIZE = 0x1A,
        /// <summary>
        /// The size of the source route table.
        /// </summary>
        EZSP_CONFIG_FRAGMENT_WINDOW_SIZE = 0x1C,
        /// <summary>
        /// window.
        /// </summary>
        EZSP_CONFIG_FRAGMENT_DELAY_MS = 0x1D,
        /// <summary>
        /// fragmented message.
        /// </summary>
        EZSP_CONFIG_KEY_TABLE_SIZE = 0x1E,
        /// <summary>
        /// normal node).
        /// </summary>
        EZSP_CONFIG_APS_ACK_TIMEOUT = 0x1F,
        /// <summary>
        /// resends of APS retried messages.
        /// </summary>
        EZSP_CONFIG_BEACON_JITTER_DURATION = 0x20,
        /// <summary>
        /// request.
        /// </summary>
        EZSP_CONFIG_PAN_ID_CONFLICT_REPORT_THRESHOLD = 0x22,
        /// <summary>
        /// manager within one minute to trigger a PAN id change.
        /// </summary>
        EZSP_CONFIG_REQUEST_KEY_TIMEOUT = 0x24,
        /// <summary>
        /// request. Zero is the most compliant behavior.
        /// </summary>
        EZSP_CONFIG_CERTIFICATE_TABLE_SIZE = 0x29,
        /// <summary>
        /// cannot be set, it can only be queried.
        /// </summary>
        EZSP_CONFIG_APPLICATION_ZDO_FLAGS = 0x2A,
        /// <summary>
        /// EMBER_APS_OPTION_ZDO_RESPONSE_REQUIRED flag is set.
        /// </summary>
        EZSP_CONFIG_BROADCAST_TABLE_SIZE = 0x2B,
        /// <summary>
        /// The maximum number of broadcasts during a single broadcast timeout period.
        /// </summary>
        EZSP_CONFIG_MAC_FILTER_TABLE_SIZE = 0x2C,
        /// <summary>
        /// The size of the MAC filter list table.
        /// </summary>
        EZSP_CONFIG_SUPPORTED_NETWORKS = 0x2D,
        /// <summary>
        /// The number of supported networks.
        /// </summary>
        EZSP_CONFIG_SEND_MULTICASTS_TO_SLEEPY_ADDRESS = 0x2E,
        /// <summary>
        /// ZigBee compliant destination for multicasts.
        /// </summary>
        EZSP_CONFIG_ZLL_GROUP_ADDRESSES = 0x2F,
        /// <summary>
        /// ZLL group address initial configuration.
        /// </summary>
        EZSP_CONFIG_ZLL_RSSI_THRESHOLD = 0x30,
        /// <summary>
        /// ZLL rssi threshold initial configuration.
        /// </summary>
        EZSP_CONFIG_MTORR_FLOW_CONTROL = 0x33,
        /// <summary>
        /// Toggles the MTORR flow control in the stack.
        /// </summary>
        EZSP_CONFIG_RETRY_QUEUE_SIZE = 0x34,
        /// <summary>
        /// sample applications is 16.
        /// </summary>
        EZSP_CONFIG_NEW_BROADCAST_ENTRY_THRESHOLD = 0x35,
        /// <summary>
        /// effectively kill this limitation.
        /// </summary>
        EZSP_CONFIG_TRANSIENT_KEY_TIMEOUT_S = 0x36,
        /// <summary>
        /// the trust center. The default value is 300 seconds, i.e., 5 minutes.
        /// </summary>
        EZSP_CONFIG_BROADCAST_MIN_ACKS_NEEDED = 0x37,
        /// <summary>
        /// stop re-transmitting broadcasts
        /// </summary>
        EZSP_CONFIG_TC_REJOINS_USING_WELL_KNOWN_KEY_TIMEOUT_S = 0x38,
        /// <summary>
        /// sli_zigbee_allow_tc_rejoins_using_well_known_key_timeout_sec value.
        /// </summary>
        EZSP_CONFIG_CTUNE_VALUE = 0x39,
        /// <summary>
        /// of the 16-bit value are ignored.
        /// </summary>
        EZSP_CONFIG_ASSUME_TC_CONCENTRATOR_TYPE = 0x40,
        /// <summary>
        /// EmberAssumeTrustCenterConcentratorType config value.
        /// </summary>
        EZSP_CONFIG_GP_PROXY_TABLE_SIZE = 0x41,
        /// <summary>
        /// set at runtime
        /// </summary>
        EZSP_CONFIG_GP_SINK_TABLE_SIZE = 0x42
    }
}
