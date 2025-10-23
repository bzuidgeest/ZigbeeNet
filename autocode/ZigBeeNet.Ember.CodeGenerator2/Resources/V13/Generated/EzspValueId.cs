// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspValueId enumeration
    /// </summary>
    public enum EzspValueId
    {
        EZSP_VALUE_TOKEN_STACK_NODE_DATA = 0x00,
        /// <summary>
        /// The contents of the node data stack token.
        /// </summary>
        EZSP_VALUE_MAC_PASSTHROUGH_FLAGS = 0x01,
        /// <summary>
        /// The types of MAC passthrough messages that the host wishes to receive.
        /// </summary>
        EZSP_VALUE_EMBERNET_PASSTHROUGH_SOURCE_ADDRESS = 0x02,
        /// <summary>
        /// EZSP_VALUE_MAC_PASSTHROUGH_FLAGS.
        /// </summary>
        EZSP_VALUE_FREE_BUFFERS = 0x03,
        /// <summary>
        /// The number of available internal RAM general purpose buffers. Read only.
        /// </summary>
        EZSP_VALUE_UART_SYNCH_CALLBACKS = 0x04,
        /// <summary>
        /// Selects sending synchronous callbacks in ezsp-uart.
        /// </summary>
        EZSP_VALUE_MAXIMUM_INCOMING_TRANSFER_SIZE = 0x05,
        /// <summary>
        /// lower byte at index 0 and upper byte at index 1.
        /// </summary>
        EZSP_VALUE_MAXIMUM_OUTGOING_TRANSFER_SIZE = 0x06,
        /// <summary>
        /// lower byte at index 0 and upper byte at index 1.
        /// </summary>
        EZSP_VALUE_STACK_TOKEN_WRITING = 0x07,
        /// <summary>
        /// they change.
        /// </summary>
        EZSP_VALUE_STACK_IS_PERFORMING_REJOIN = 0x08,
        /// <summary>
        /// rejoin.
        /// </summary>
        EZSP_VALUE_MAC_FILTER_LIST = 0x09,
        /// <summary>
        /// A list of EmberMacFilterMatchData values.
        /// </summary>
        EZSP_VALUE_EXTENDED_SECURITY_BITMASK = 0x0A,
        /// <summary>
        /// The Ember Extended Security Bitmask.
        /// </summary>
        EZSP_VALUE_NODE_SHORT_ID = 0x0B,
        /// <summary>
        /// The node short ID.
        /// </summary>
        EZSP_VALUE_DESCRIPTOR_CAPABILITY = 0x0C,
        /// <summary>
        /// The descriptor capability of the local node. Write only.
        /// </summary>
        EZSP_VALUE_STACK_DEVICE_REQUEST_SEQUENCE_NUMBER = 0x0D,
        /// <summary>
        /// The stack device request sequence number of the local node.
        /// </summary>
        EZSP_VALUE_RADIO_HOLD_OFF = 0x0E,
        /// <summary>
        /// Enable or disable radio hold-off.
        /// </summary>
        EZSP_VALUE_ENDPOINT_FLAGS = 0x0F,
        /// <summary>
        /// The flags field associated with the endpoint data.
        /// </summary>
        EZSP_VALUE_MFG_SECURITY_CONFIG = 0x10,
        /// <summary>
        /// Enable/disable the Mfg security config key settings.
        /// </summary>
        EZSP_VALUE_VERSION_INFO = 0x11,
        /// <summary>
        /// Retrieves the version information from the stack on the NCP.
        /// </summary>
        EZSP_VALUE_NEXT_HOST_REJOIN_REASON = 0x12,
        /// <summary>
        /// do anything with this value other than cache it so you can read it later.
        /// </summary>
        EZSP_VALUE_LAST_REJOIN_REASON = 0x13,
        /// <summary>
        /// internally for retrieval by ezspGetValue(EZSP_VALUE_REAL_REJOIN_REASON).
        /// </summary>
        EZSP_VALUE_NEXT_ZIGBEE_SEQUENCE_NUMBER = 0x14,
        /// <summary>
        /// The next ZigBee sequence number.
        /// </summary>
        EZSP_VALUE_CCA_THRESHOLD = 0x15,
        /// <summary>
        /// CCA energy detect threshold for radio.
        /// </summary>
        EZSP_VALUE_SET_COUNTER_THRESHOLD = 0x17,
        /// <summary>
        /// The threshold value for a counter
        /// </summary>
        EZSP_VALUE_RESET_COUNTER_THRESHOLDS = 0x18,
        /// <summary>
        /// Resets all counters thresholds to 0xFF
        /// </summary>
        EZSP_VALUE_CLEAR_COUNTERS = 0x19,
        /// <summary>
        /// Clears all the counters
        /// </summary>
        EZSP_VALUE_CERTIFICATE_283K1 = 0x1A,
        /// <summary>
        /// The node's new certificate signed by the CA.
        /// </summary>
        EZSP_VALUE_PUBLIC_KEY_283K1 = 0x1B,
        /// <summary>
        /// The Certificate Authority's public key.
        /// </summary>
        EZSP_VALUE_PRIVATE_KEY_283K1 = 0x1C,
        /// <summary>
        /// The node's new static private key.
        /// </summary>
        EZSP_VALUE_NWK_FRAME_COUNTER = 0x23,
        /// <summary>
        /// The NWK layer security frame counter value
        /// </summary>
        EZSP_VALUE_APS_FRAME_COUNTER = 0x24,
        /// <summary>
        /// should not set these unless doing backup and restore.
        /// </summary>
        EZSP_VALUE_RETRY_DEVICE_TYPE = 0x25,
        /// <summary>
        /// Sets the device type to use on the next rejoin using device type
        /// </summary>
        EZSP_VALUE_ENABLE_R21_BEHAVIOR = 0x29,
        /// <summary>
        /// Setting this byte enables R21 behavior on the NCP.
        /// </summary>
        EZSP_VALUE_ANTENNA_MODE = 0x30,
        /// <summary>
        /// antenna diversity).
        /// </summary>
        EZSP_VALUE_ENABLE_PTA = 0x31,
        /// <summary>
        /// Enable or disable packet traffic arbitration.
        /// </summary>
        EZSP_VALUE_PTA_OPTIONS = 0x32,
        /// <summary>
        /// Set packet traffic arbitration configuration options.
        /// </summary>
        EZSP_VALUE_MFGLIB_OPTIONS = 0x33,
        /// <summary>
        /// transmits). To be used with Manufacturing Library.
        /// </summary>
        EZSP_VALUE_USE_NEGOTIATED_POWER_BY_LPD = 0x34,
        /// <summary>
        /// purposes.
        /// </summary>
        EZSP_VALUE_PTA_PWM_OPTIONS = 0x35,
        /// <summary>
        /// Set packet traffic arbitration PWM options.
        /// </summary>
        EZSP_VALUE_PTA_DIRECTIONAL_PRIORITY_PULSE_WIDTH = 0x36,
        /// <summary>
        /// microseconds.
        /// </summary>
        EZSP_VALUE_PTA_PHY_SELECT_TIMEOUT = 0x37,
        /// <summary>
        /// Set packet traffic arbitration phy select timeout(ms).
        /// </summary>
        EZSP_VALUE_ANTENNA_RX_MODE = 0x38,
        /// <summary>
        /// 3-RX antenna diversity).
        /// </summary>
        EZSP_VALUE_NWK_KEY_TIMEOUT = 0x39,
        /// <summary>
        /// Acceptable timeout range [3,255]. Value is in seconds.
        /// </summary>
        EZSP_VALUE_FORCE_TX_AFTER_FAILED_CCA_ATTEMPTS = 0x3A,
        /// <summary>
        /// the feature.
        /// </summary>
        EZSP_VALUE_TRANSIENT_KEY_TIMEOUT_S = 0x3B,
        /// <summary>
        /// minutes).
        /// </summary>
        EZSP_VALUE_COULOMB_COUNTER_USAGE = 0x3C,
        /// <summary>
        /// counter plugin. Setting this value will reset the coulomb counter.
        /// </summary>
        EZSP_VALUE_MAX_BEACONS_TO_STORE = 0x3D,
        /// <summary>
        /// Each beacon consumes one packet buffer in RAM.
        /// </summary>
        EZSP_VALUE_END_DEVICE_TIMEOUT_OPTIONS_MASK = 0x3E,
        /// <summary>
        /// Set the mask to filter out unacceptable child timeout options on a router.
        /// </summary>
        EZSP_VALUE_END_DEVICE_KEEP_ALIVE_SUPPORT_MODE = 0x3F,
        /// <summary>
        /// The end device keep-alive mode supported by the parent.
        /// </summary>
        EZSP_VALUE_ACTIVE_RADIO_CONFIG = 0x41,
        /// <summary>
        /// Antenna Diversity, 2: Co-Existence, 3: Antenna Diversity and Co-Existence.
        /// </summary>
        EZSP_VALUE_NWK_OPEN_DURATION = 0x42,
        /// <summary>
        /// of 0 indicates that the network is closed. Read only.
        /// </summary>
        EZSP_VALUE_TRANSIENT_DEVICE_TIMEOUT = 0x43,
        /// <summary>
        /// purged
        /// </summary>
        EZSP_VALUE_KEY_STORAGE_VERSION = 0x44,
        /// <summary>
        /// only.
        /// </summary>
        EZSP_VALUE_DELAYED_JOIN_ACTIVATION = 0x45
    }
}
