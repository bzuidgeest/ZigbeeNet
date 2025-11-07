#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Identifies a value.
/// </summary>
public enum ZigbeeEzspValueId : byte
{
	/// <summary>
	/// The contents of the node data stack token.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_TOKEN_STACK_NODE_DATA = 0x00,
	/// <summary>
	/// The types of MAC passthrough messages that the host wishes to receive.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAC_PASSTHROUGH_FLAGS = 0x01,
	/// <summary>
	/// The source address used to filter legacy EmberNet messages when the SL_802154_PASSTHROUGH_EMBERNET_SOURCE flag is set in SL_ZIGBEE_EZSP_VALUE_MAC_PASSTHROUGH_FLAGS.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_EMBERNET_PASSTHROUGH_SOURCE_ADDRESS = 0x02,
	/// <summary>
	/// The amount in bytes (max 2^16) of available general purpose heap memory
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_BUFFER_HEAP_FREE_SIZE = 0x03,
	/// <summary>
	/// Selects sending synchronous callbacks in ezsp-uart.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_UART_SYNCH_CALLBACKS = 0x04,
	/// <summary>
	/// The maximum incoming transfer size for the local node. Default value is set to 82 and does not use fragmentation. Sets the value in Node Descriptor. To set, this takes the input of a uint8 array of length 2 where you pass the lower byte at index 0 and upper byte at index 1.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAXIMUM_INCOMING_TRANSFER_SIZE = 0x05,
	/// <summary>
	/// The maximum outgoing transfer size for the local node. Default value is set to 82 and does not use fragmentation. Sets the value in Node Descriptor. To set, this takes the input of a uint8 array of length 2 where you pass the lower byte at index 0 and upper byte at index 1.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAXIMUM_OUTGOING_TRANSFER_SIZE = 0x06,
	/// <summary>
	/// A bool indicating whether stack tokens are written to persistent storage as they change.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_STACK_TOKEN_WRITING = 0x07,
	/// <summary>
	/// A read-only value indicating whether the stack is currently performing a rejoin.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_STACK_IS_PERFORMING_REJOIN = 0x08,
	/// <summary>
	/// A list of sl_zigbee_mac_filter_match_data_t values.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAC_FILTER_LIST = 0x09,
	/// <summary>
	/// The Ember Extended Security Bitmask.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_EXTENDED_SECURITY_BITMASK = 0x0A,
	/// <summary>
	/// The node short ID.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_NODE_SHORT_ID = 0x0B,
	/// <summary>
	/// The descriptor capability of the local node. Write only.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_DESCRIPTOR_CAPABILITY = 0x0C,
	/// <summary>
	/// The stack device request sequence number of the local node.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_STACK_DEVICE_REQUEST_SEQUENCE_NUMBER = 0x0D,
	/// <summary>
	/// Enable or disable radio hold-off.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_RADIO_HOLD_OFF = 0x0E,
	/// <summary>
	/// The flags field associated with the endpoint data.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ENDPOINT_FLAGS = 0x0F,
	/// <summary>
	/// Enable/disable the Mfg security config key settings.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MFG_SECURITY_CONFIG = 0x10,
	/// <summary>
	/// Retrieves the version information from the stack on the NCP.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_VERSION_INFO = 0x11,
	/// <summary>
	/// This is the reason that the last rejoin took place. This value may only be retrieved, not set. The rejoin may have been initiated by the stack (NCP) or the application (host). If a host initiated a rejoin the reason will be set by default to SL_ZIGBEE_REJOIN_DUE_TO_APP_EVENT_1. If the application wishes to denote its own rejoin reasons it can do so by calling sl_zigbee_ezsp_set_value(EMBER_VALUE_HOST_REJOIN_REASON, SL_ZIGBEE_REJOIN_DUE_TO_APP_EVENT_X). X is a number corresponding to one of the app events defined. If the NCP initiated a rejoin it will record this value internally for retrieval by sl_zigbee_ezsp_get_value(EZSP_VALUE_REAL_REJOIN_REASON).
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_LAST_REJOIN_REASON = 0x13,
	/// <summary>
	/// The next ZigBee sequence number.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_NEXT_ZIGBEE_SEQUENCE_NUMBER = 0x14,
	/// <summary>
	/// CCA energy detect threshold for radio.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_CCA_THRESHOLD = 0x15,
	/// <summary>
	/// The threshold value for a counter
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_SET_COUNTER_THRESHOLD = 0x17,
	/// <summary>
	/// Resets all counters thresholds to 0xFF
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_RESET_COUNTER_THRESHOLDS = 0x18,
	/// <summary>
	/// Clears all the counters
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_CLEAR_COUNTERS = 0x19,
	/// <summary>
	/// The node&apos;s new certificate signed by the CA.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_CERTIFICATE_283K1 = 0x1A,
	/// <summary>
	/// The Certificate Authority&apos;s public key.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PUBLIC_KEY_283K1 = 0x1B,
	/// <summary>
	/// The node&apos;s new static private key.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PRIVATE_KEY_283K1 = 0x1C,
	/// <summary>
	/// The NWK layer security frame counter value
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_NWK_FRAME_COUNTER = 0x23,
	/// <summary>
	/// The APS layer security frame counter value. Managed by the stack. Users should not set these unless doing backup and restore.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_APS_FRAME_COUNTER = 0x24,
	/// <summary>
	/// Sets the device type to use on the next rejoin using device type
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_RETRY_DEVICE_TYPE = 0x25,
	/// <summary>
	/// Setting this byte enables R21 behavior on the NCP.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ENABLE_R21_BEHAVIOR = 0x29,
	/// <summary>
	/// Configure the antenna mode(0-don&apos;t switch,1-primary,2-secondary,3-TX antenna diversity).
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ANTENNA_MODE = 0x30,
	/// <summary>
	/// Enable or disable packet traffic arbitration.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ENABLE_PTA = 0x31,
	/// <summary>
	/// Set packet traffic arbitration configuration options.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PTA_OPTIONS = 0x32,
	/// <summary>
	/// Configure manufacturing library options (0-non-CSMA transmits,1-CSMA transmits). To be used with Manufacturing Library.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MFGLIB_OPTIONS = 0x33,
	/// <summary>
	/// Sets the flag to use either negotiated power by link power delta (LPD) or fixed power value provided by user while forming/joining a network for packet transmissions on sub-ghz interface. This is mainly for testing purposes.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_USE_NEGOTIATED_POWER_BY_LPD = 0x34,
	/// <summary>
	/// Set packet traffic arbitration PWM options.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PTA_PWM_OPTIONS = 0x35,
	/// <summary>
	/// Set packet traffic arbitration directional priority pulse width in microseconds.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PTA_DIRECTIONAL_PRIORITY_PULSE_WIDTH = 0x36,
	/// <summary>
	/// Set packet traffic arbitration phy select timeout(ms).
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_PTA_PHY_SELECT_TIMEOUT = 0x37,
	/// <summary>
	/// Configure the RX antenna mode: (0-do not switch; 1-primary; 2-secondary; 3-RX antenna diversity).
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ANTENNA_RX_MODE = 0x38,
	/// <summary>
	/// Configure the timeout to wait for the network key before failing a join. Acceptable timeout range [3,255]. Value is in seconds.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_NWK_KEY_TIMEOUT = 0x39,
	/// <summary>
	/// The number of failed CSMA attempts due to failed CCA made by the MAC before continuing transmission with CCA disabled.  This is the same as calling the sli_zigbee_stack_force_tx_after_failed_cca(uint8_t csmaAttempts) API. A value of 0 disables the feature.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_FORCE_TX_AFTER_FAILED_CCA_ATTEMPTS = 0x3A,
	/// <summary>
	/// The length of time, in seconds, that a trust center will store a transient link key that a device can use to join its network. A transient key is added with a call to sli_zigbee_stack_sec_man_import_transient_key. After the transient key is added, it will be removed once this amount of time has passed. A joining device will not be able to use that key to join until it is added again on the trust center. The default value is 300 seconds (5 minutes).
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_TRANSIENT_KEY_TIMEOUT_S = 0x3B,
	/// <summary>
	/// Cumulative energy usage metric since the last value reset of the coulomb counter plugin. Setting this value will reset the coulomb counter.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_COULOMB_COUNTER_USAGE = 0x3C,
	/// <summary>
	/// When scanning, configure the maximum number of beacons to store in cache. Each beacon consumes on average 32-bytes (+ buffer overhead) in RAM.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAX_BEACONS_TO_STORE = 0x3D,
	/// <summary>
	/// Set the mask to filter out unacceptable child timeout options on a router.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_END_DEVICE_TIMEOUT_OPTIONS_MASK = 0x3E,
	/// <summary>
	/// The end device keep-alive mode supported by the parent.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_END_DEVICE_KEEP_ALIVE_SUPPORT_MODE = 0x3F,
	/// <summary>
	/// Return the active radio config. Read only. Values are 0: Default, 1: Antenna Diversity, 2: Co-Existence, 3: Antenna Diversity and Co-Existence.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_ACTIVE_RADIO_CONFIG = 0x41,
	/// <summary>
	/// Return the number of seconds the network will remain open. A return value of 0 indicates that the network is closed. Read only.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_NWK_OPEN_DURATION = 0x42,
	/// <summary>
	/// Timeout in milliseconds to store entries in the transient device table. If the devices are not authenticated before the timeout, the entry shall be purged
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_TRANSIENT_DEVICE_TIMEOUT = 0x43,
	/// <summary>
	/// Return information about the key storage on an NCP.  Returns 0 if keys are in classic key storage, and 1 if they are located in PSA key storage. Read only.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_KEY_STORAGE_VERSION = 0x44,
	/// <summary>
	/// Return activation state about TC Delayed Join on an NCP.  A return value of 0 indicates that the feature is not activated.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_DELAYED_JOIN_ACTIVATION = 0x45,
	/// <summary>
	/// The maximum number of NWK retries that will be attempted.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_MAX_NWK_RETRIES = 0x46,
	/// <summary>
	/// Policies for allowing/disallowing rejoins.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_REJOIN_MODE = 0x47,
	/// <summary>
	/// Controls whether devices must use an install code when joining.
	/// </summary>
    SL_ZIGBEE_EZSP_VALUE_JOIN_USE_INSTALL_CODE_ENABLE = 0x48
}

#endif