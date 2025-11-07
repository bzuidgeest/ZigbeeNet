#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Identifies a configuration value.
/// </summary>
public enum ZigbeeEzspConfigId : byte
{
	/// <summary>
	/// The NCP no longer supports configuration of packet buffer heap at runtime using this parameter. Packet buffer heap space must be configured using the SL_ZIGBEE_PACKET_BUFFER_HEAP_SIZE macro when building the NCP project.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_PACKET_BUFFER_HEAP_SIZE = 0x01,
	/// <summary>
	/// The maximum number of router neighbors the stack can keep track of. A neighbor is a node within radio range.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_NEIGHBOR_TABLE_SIZE = 0x02,
	/// <summary>
	/// The maximum number of APS retried messages the stack can be transmitting at any time.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_APS_UNICAST_MESSAGE_COUNT = 0x03,
	/// <summary>
	/// The maximum number of non-volatile bindings supported by the stack.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_BINDING_TABLE_SIZE = 0x04,
	/// <summary>
	/// The maximum number of EUI64 to network address associations that the stack can maintain for the application. (Note, the total number of such address associations maintained by the NCP is the sum of the value of this setting and the value of ::SL_ZIGBEE_EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_ADDRESS_TABLE_SIZE = 0x05,
	/// <summary>
	/// The maximum number of multicast groups that the device may be a member of.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_MULTICAST_TABLE_SIZE = 0x06,
	/// <summary>
	/// The maximum number of destinations to which a node can route messages. This includes both messages originating at this node and those relayed for others.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_ROUTE_TABLE_SIZE = 0x07,
	/// <summary>
	/// The number of simultaneous route discoveries that a node will support.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_DISCOVERY_TABLE_SIZE = 0x08,
	/// <summary>
	/// Specifies the stack profile.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_STACK_PROFILE = 0x0C,
	/// <summary>
	/// The security level used for security at the MAC and network layers. The supported values are 0 (no security) and 5 (payload is encrypted and a four-byte MIC is used for authentication).
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_SECURITY_LEVEL = 0x0D,
	/// <summary>
	/// The maximum number of hops for a message.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_MAX_HOPS = 0x10,
	/// <summary>
	/// The maximum number of end device children that a router will support.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_MAX_END_DEVICE_CHILDREN = 0x11,
	/// <summary>
	/// The maximum amount of time that the MAC will hold a message for indirect transmission to a child.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_INDIRECT_TRANSMISSION_TIMEOUT = 0x12,
	/// <summary>
	/// The maximum amount of time that an end device child can wait between polls. If no poll is heard within this timeout, then the parent removes the end device from its tables. Value range 0-14. The timeout corresponding to a value of zero is 10 seconds. The timeout corresponding to a nonzero value N is 2^N minutes, ranging from 2^1 = 2 minutes to 2^14 = 16384 minutes.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_END_DEVICE_POLL_TIMEOUT = 0x13,
	/// <summary>
	/// Enables boost power mode and/or the alternate transmitter output.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_TX_POWER_MODE = 0x17,
	/// <summary>
	/// 0: Allow this node to relay messages. 1: Prevent this node from relaying messages.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_DISABLE_RELAY = 0x18,
	/// <summary>
	/// The maximum number of EUI64 to network address associations that the Trust Center can maintain. These address cache entries are reserved for and reused by the Trust Center when processing device join/rejoin authentications. This cache size limits the number of overlapping joins the Trust Center can process within a narrow time window (e.g. two seconds), and thus should be set to the maximum number of near simultaneous joins the Trust Center is expected to accommodate. (Note, the total number of such address associations maintained by the NCP is the sum of the value of this setting and the value of ::SL_ZIGBEE_EZSP_CONFIG_ADDRESS_TABLE_SIZE.)
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_TRUST_CENTER_ADDRESS_CACHE_SIZE = 0x19,
	/// <summary>
	/// The size of the source route table.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_SOURCE_ROUTE_TABLE_SIZE = 0x1A,
	/// <summary>
	/// The number of blocks of a fragmented message that can be sent in a single window.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_FRAGMENT_WINDOW_SIZE = 0x1C,
	/// <summary>
	/// The time the stack will wait (in milliseconds) between sending blocks of a fragmented message.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_FRAGMENT_DELAY_MS = 0x1D,
	/// <summary>
	/// The size of the Key Table used for storing individual link keys (if the device is a Trust Center) or Application Link Keys (if the device is a normal node).
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_KEY_TABLE_SIZE = 0x1E,
	/// <summary>
	/// The APS ACK timeout value. The stack waits this amount of time between resends of APS retried messages.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_APS_ACK_TIMEOUT = 0x1F,
	/// <summary>
	/// The duration of a beacon jitter, in the units used by the 15.4 scan parameter (((1 &lt;&lt; duration) + 1) * 15ms), when responding to a beacon request.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_BEACON_JITTER_DURATION = 0x20,
	/// <summary>
	/// The number of PAN id conflict reports that must be received by the network manager within one minute to trigger a PAN id change.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_PAN_ID_CONFLICT_REPORT_THRESHOLD = 0x22,
	/// <summary>
	/// The timeout value in minutes for how long the Trust Center or a normal node waits for the ZigBee Request Key to complete. On the Trust Center this controls whether or not the device buffers the request, waiting for a matching pair of ZigBee Request Key. If the value is non-zero, the Trust Center buffers and waits for that amount of time. If the value is zero, the Trust Center does not buffer the request and immediately responds to the request. Zero is the most compliant behavior.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_REQUEST_KEY_TIMEOUT = 0x24,
	/// <summary>
	/// This value indicates the size of the runtime modifiable certificate table. Normally certificates are stored in MFG tokens but this table can be used to field upgrade devices with new Smart Energy certificates. This value cannot be set, it can only be queried.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_CERTIFICATE_TABLE_SIZE = 0x29,
	/// <summary>
	/// This is a bitmask that controls which incoming ZDO request messages are passed to the application. The bits are defined in the sl_zigbee_zdo_configuration_flags_t enumeration. To see if the application is required to send a ZDO response in reply to an incoming message, the application must check the APS options bitfield within the incomingMessageHandler callback to see if the SL_ZIGBEE_APS_OPTION_ZDO_RESPONSE_REQUIRED flag is set.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_APPLICATION_ZDO_FLAGS = 0x2A,
	/// <summary>
	/// The maximum number of broadcasts during a single broadcast timeout period.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_BROADCAST_TABLE_SIZE = 0x2B,
	/// <summary>
	/// The size of the MAC filter list table.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_MAC_FILTER_TABLE_SIZE = 0x2C,
	/// <summary>
	/// The number of supported networks.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_SUPPORTED_NETWORKS = 0x2D,
	/// <summary>
	/// Whether multicasts are sent to the RxOnWhenIdle=true address (0xFFFD) or the sleepy broadcast address (0xFFFF). The RxOnWhenIdle=true address is the ZigBee compliant destination for multicasts.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_SEND_MULTICASTS_TO_SLEEPY_ADDRESS = 0x2E,
	/// <summary>
	/// ZLL group address initial configuration.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_ZLL_GROUP_ADDRESSES = 0x2F,
	/// <summary>
	/// ZLL rssi threshold initial configuration.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_ZLL_RSSI_THRESHOLD = 0x30,
	/// <summary>
	/// Toggles the MTORR flow control in the stack.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_MTORR_FLOW_CONTROL = 0x33,
	/// <summary>
	/// Setting the retry queue size. Applies to all queues. Default value in the sample applications is 16.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_RETRY_QUEUE_SIZE = 0x34,
	/// <summary>
	/// Setting the new broadcast entry threshold. The number (BROADCAST_TABLE_SIZE - NEW_BROADCAST_ENTRY_THRESHOLD) of broadcast table entries are reserved for relaying the broadcast messages originated on other devices. The local device will fail to originate a broadcast message after this threshold is reached. Setting this value to BROADCAST_TABLE_SIZE and greater will effectively kill this limitation.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_NEW_BROADCAST_ENTRY_THRESHOLD = 0x35,
	/// <summary>
	/// &lt;Deprecated: See SL_ZIGBEE_EZSP_VALUE_TRANSIENT_KEY_TIMEOUT_S&gt; The length of time, in seconds, that a trust center will store a transient link key that a device can use to join its network. A transient key is added with a call to emberAddTransientLinkKey. After the transient key is added, it will be removed once this amount of time has passed. A joining device will not be able to use that key to join until it is added again on the trust center. The default value is 300 seconds, i.e., 5 minutes.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_TRANSIENT_KEY_TIMEOUT_S = 0x36,
	/// <summary>
	/// The number of passive acknowledgements to record from neighbors before we stop re-transmitting broadcasts
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_BROADCAST_MIN_ACKS_NEEDED = 0x37,
	/// <summary>
	/// The length of time, in seconds, that a trust center will allow a Trust Center (insecure) rejoin for a device that is using the well-known link key. This timeout takes effect once rejoins using the well-known key has been allowed. This command updates the sli_zigbee_allow_tc_rejoins_using_well_known_key_timeout_sec value.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_TC_REJOINS_USING_WELL_KNOWN_KEY_TIMEOUT_S = 0x38,
	/// <summary>
	/// Valid range of a CTUNE value is 0x0000-0x01FF. Higher order bits (0xFE00) of the 16-bit value are ignored. Note setting this parameter using ezsp API for setting configuration value invokes internal manufacturer library function call, mfglibInternalSetCtune, unlike a zigbee stack call, and hence the return value of the ezsp set API should be interpreted of type sl_status_t type.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_CTUNE_VALUE = 0x39,
	/// <summary>
	/// To configure non trust center node to assume a concentrator type of the trust center it join to, until it receive many-to-one route request from the trust center. For the trust center node, concentrator type is configured from the concentrator plugin. The stack by default assumes trust center be a low RAM concentrator that make other devices send route record to the trust center even without receiving a many-to-one route request. The default concentrator type can be changed by setting appropriate sl_zigbee_assume_trust_center_concentrator_type_t config value.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_ASSUME_TC_CONCENTRATOR_TYPE = 0x40,
	/// <summary>
	/// This is green power proxy table size. This value is read-only and cannot be set at runtime
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_GP_PROXY_TABLE_SIZE = 0x41,
	/// <summary>
	/// This is green power sink table size. This value is read-only and cannot be set at runtime
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_GP_SINK_TABLE_SIZE = 0x42,
	/// <summary>
	/// This is The configuration advertised by the end device to the parent when joining/rejoining, either SL_ZIGBEE_END_DEVICE_CONFIG_NONE or SL_ZIGBEE_END_DEVICE_CONFIG_PERSIST_DATA_ON_PARENT.
	/// </summary>
    SL_ZIGBEE_EZSP_CONFIG_END_DEVICE_CONFIGURATION = 0x43
}

#endif