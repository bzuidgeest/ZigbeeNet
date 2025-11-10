#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// See sl_status.h for an enumerated list.
/// </summary>
public enum Status : uint
{
	/// <summary>
	/// sl status space mask.
	/// </summary>
	SL_STATUS_SPACE_MASK = 0xFF00,
	/// <summary>
	/// sl status generic space.
	/// </summary>
	SL_STATUS_GENERIC_SPACE = 0x0000,
	/// <summary>
	/// sl status platform 1 space.
	/// </summary>
	SL_STATUS_PLATFORM_1_SPACE = 0x0100,
	/// <summary>
	/// sl status platform 2 space.
	/// </summary>
	SL_STATUS_PLATFORM_2_SPACE = 0x0200,
	/// <summary>
	/// sl status hardware space.
	/// </summary>
	SL_STATUS_HARDWARE_SPACE = 0x0300,
	/// <summary>
	/// sl status bluetooth space.
	/// </summary>
	SL_STATUS_BLUETOOTH_SPACE = 0x0400,
	/// <summary>
	/// sl status bluetooth mesh space.
	/// </summary>
	SL_STATUS_BLUETOOTH_MESH_SPACE = 0x0500,
	/// <summary>
	/// sl status can canopen space.
	/// </summary>
	SL_STATUS_CAN_CANOPEN_SPACE = 0x0600,
	/// <summary>
	/// sl status connect space.
	/// </summary>
	SL_STATUS_CONNECT_SPACE = 0x0700,
	/// <summary>
	/// sl status net suite space.
	/// </summary>
	SL_STATUS_NET_SUITE_SPACE = 0x0800,
	/// <summary>
	/// sl status thread space.
	/// </summary>
	SL_STATUS_THREAD_SPACE = 0x0900,
	/// <summary>
	/// sl status usb space.
	/// </summary>
	SL_STATUS_USB_SPACE = 0x0A00,
	/// <summary>
	/// sl status wifi space.
	/// </summary>
	SL_STATUS_WIFI_SPACE = 0x0B00,
	/// <summary>
	/// sl status zigbee space.
	/// </summary>
	SL_STATUS_ZIGBEE_SPACE = 0x0C00,
	/// <summary>
	/// sl status z wave space.
	/// </summary>
	SL_STATUS_Z_WAVE_SPACE = 0x0D00,
	/// <summary>
	/// sl status gecko os 1 space.
	/// </summary>
	SL_STATUS_GECKO_OS_1_SPACE = 0x0E00,
	/// <summary>
	/// sl status gecko os 2 space.
	/// </summary>
	SL_STATUS_GECKO_OS_2_SPACE = 0x0F00,
	/// <summary>
	/// sl status bluetooth ctrl space.
	/// </summary>
	SL_STATUS_BLUETOOTH_CTRL_SPACE = 0x1000,
	/// <summary>
	/// sl status bluetooth att space.
	/// </summary>
	SL_STATUS_BLUETOOTH_ATT_SPACE = 0x1100,
	/// <summary>
	/// sl status bluetooth mesh foundation space.
	/// </summary>
	SL_STATUS_BLUETOOTH_SMP_SPACE = 0x1200,
	/// <summary>
	/// sl status bluetooth mesh foundation space.
	/// </summary>
	SL_STATUS_BLUETOOTH_MESH_FOUNDATION_SPACE = 0x1300,
	/// <summary>
	/// sl status wisun space.
	/// </summary>
	SL_STATUS_WISUN_SPACE = 0x1400,
	/// <summary>
	/// sl status compute space.
	/// </summary>
	SL_STATUS_COMPUTE_SPACE = 0x1500,
	/// <summary>
	/// No error.
	/// </summary>
	SL_STATUS_OK = 0x0000,
	/// <summary>
	/// Generic error.
	/// </summary>
	SL_STATUS_FAIL = 0x0001,
	/// <summary>
	/// Generic invalid state error.
	/// </summary>
	SL_STATUS_INVALID_STATE = 0x0002,
	/// <summary>
	/// Module is not ready for requested operation.
	/// </summary>
	SL_STATUS_NOT_READY = 0x0003,
	/// <summary>
	/// Module is busy and cannot carry out requested operation.
	/// </summary>
	SL_STATUS_BUSY = 0x0004,
	/// <summary>
	/// Operation is in progress and not yet complete (pass or fail).
	/// </summary>
	SL_STATUS_IN_PROGRESS = 0x0005,
	/// <summary>
	/// Operation aborted.
	/// </summary>
	SL_STATUS_ABORT = 0x0006,
	/// <summary>
	/// Operation timed out.
	/// </summary>
	SL_STATUS_TIMEOUT = 0x0007,
	/// <summary>
	/// Operation not allowed per permissions.
	/// </summary>
	SL_STATUS_PERMISSION = 0x0008,
	/// <summary>
	/// Non-blocking operation would block.
	/// </summary>
	SL_STATUS_WOULD_BLOCK = 0x0009,
	/// <summary>
	/// Operation/module is Idle, cannot carry requested operation.
	/// </summary>
	SL_STATUS_IDLE = 0x000A,
	/// <summary>
	/// Operation cannot be done while construct is waiting.
	/// </summary>
	SL_STATUS_IS_WAITING = 0x000B,
	/// <summary>
	/// No task/construct waiting/pending for that action/event.
	/// </summary>
	SL_STATUS_NONE_WAITING = 0x000C,
	/// <summary>
	/// Operation cannot be done while construct is suspended.
	/// </summary>
	SL_STATUS_SUSPENDED = 0x000D,
	/// <summary>
	/// Feature not available due to software configuration.
	/// </summary>
	SL_STATUS_NOT_AVAILABLE = 0x000E,
	/// <summary>
	/// Feature not supported.
	/// </summary>
	SL_STATUS_NOT_SUPPORTED = 0x000F,
	/// <summary>
	/// Initialization failed.
	/// </summary>
	SL_STATUS_INITIALIZATION = 0x0010,
	/// <summary>
	/// Module has not been initialized.
	/// </summary>
	SL_STATUS_NOT_INITIALIZED = 0x0011,
	/// <summary>
	/// Module has already been initialized.
	/// </summary>
	SL_STATUS_ALREADY_INITIALIZED = 0x0012,
	/// <summary>
	/// Object/construct has been deleted.
	/// </summary>
	SL_STATUS_DELETED = 0x0013,
	/// <summary>
	/// Illegal call from ISR.
	/// </summary>
	SL_STATUS_ISR = 0x0014,
	/// <summary>
	/// Illegal call because network is up.
	/// </summary>
	SL_STATUS_NETWORK_UP = 0x0015,
	/// <summary>
	/// Illegal call because network is down.
	/// </summary>
	SL_STATUS_NETWORK_DOWN = 0x0016,
	/// <summary>
	/// Failure due to not being joined in a network.
	/// </summary>
	SL_STATUS_NOT_JOINED = 0x0017,
	/// <summary>
	/// Invalid operation as there are no beacons.
	/// </summary>
	SL_STATUS_NO_BEACONS = 0x0018,
	/// <summary>
	/// Generic allocation error.
	/// </summary>
	SL_STATUS_ALLOCATION_FAILED = 0x0019,
	/// <summary>
	/// No more resource available to perform the operation.
	/// </summary>
	SL_STATUS_NO_MORE_RESOURCE = 0x001A,
	/// <summary>
	/// Item/list/queue is empty.
	/// </summary>
	SL_STATUS_EMPTY = 0x001B,
	/// <summary>
	/// Item/list/queue is full.
	/// </summary>
	SL_STATUS_FULL = 0x001C,
	/// <summary>
	/// Item would overflow.
	/// </summary>
	SL_STATUS_WOULD_OVERFLOW = 0x001D,
	/// <summary>
	/// Item/list/queue has been overflowed.
	/// </summary>
	SL_STATUS_HAS_OVERFLOWED = 0x001E,
	/// <summary>
	/// Generic ownership error.
	/// </summary>
	SL_STATUS_OWNERSHIP = 0x001F,
	/// <summary>
	/// Already/still owning resource.
	/// </summary>
	SL_STATUS_IS_OWNER = 0x0020,
	/// <summary>
	/// Generic invalid argument or consequence of invalid argument.
	/// </summary>
	SL_STATUS_INVALID_PARAMETER = 0x0021,
	/// <summary>
	/// Invalid null pointer received as argument.
	/// </summary>
	SL_STATUS_NULL_POINTER = 0x0022,
	/// <summary>
	/// Invalid configuration provided.
	/// </summary>
	SL_STATUS_INVALID_CONFIGURATION = 0x0023,
	/// <summary>
	/// Invalid mode.
	/// </summary>
	SL_STATUS_INVALID_MODE = 0x0024,
	/// <summary>
	/// Invalid handle.
	/// </summary>
	SL_STATUS_INVALID_HANDLE = 0x0025,
	/// <summary>
	/// Invalid type for operation.
	/// </summary>
	SL_STATUS_INVALID_TYPE = 0x0026,
	/// <summary>
	/// Invalid index.
	/// </summary>
	SL_STATUS_INVALID_INDEX = 0x0027,
	/// <summary>
	/// Invalid range.
	/// </summary>
	SL_STATUS_INVALID_RANGE = 0x0028,
	/// <summary>
	/// Invalid key.
	/// </summary>
	SL_STATUS_INVALID_KEY = 0x0029,
	/// <summary>
	/// Invalid credentials.
	/// </summary>
	SL_STATUS_INVALID_CREDENTIALS = 0x002A,
	/// <summary>
	/// Invalid count.
	/// </summary>
	SL_STATUS_INVALID_COUNT = 0x002B,
	/// <summary>
	/// Invalid signature / verification failed.
	/// </summary>
	SL_STATUS_INVALID_SIGNATURE = 0x002C,
	/// <summary>
	/// Item could not be found.
	/// </summary>
	SL_STATUS_NOT_FOUND = 0x002D,
	/// <summary>
	/// Item already exists.
	/// </summary>
	SL_STATUS_ALREADY_EXISTS = 0x002E,
	/// <summary>
	/// Generic I/O failure.
	/// </summary>
	SL_STATUS_IO = 0x002F,
	/// <summary>
	/// I/O failure due to timeout.
	/// </summary>
	SL_STATUS_IO_TIMEOUT = 0x0030,
	/// <summary>
	/// Generic transmission error.
	/// </summary>
	SL_STATUS_TRANSMIT = 0x0031,
	/// <summary>
	/// Transmit underflowed.
	/// </summary>
	SL_STATUS_TRANSMIT_UNDERFLOW = 0x0032,
	/// <summary>
	/// Transmit is incomplete.
	/// </summary>
	SL_STATUS_TRANSMIT_INCOMPLETE = 0x0033,
	/// <summary>
	/// Transmit is busy.
	/// </summary>
	SL_STATUS_TRANSMIT_BUSY = 0x0034,
	/// <summary>
	/// Generic reception error.
	/// </summary>
	SL_STATUS_RECEIVE = 0x0035,
	/// <summary>
	/// Failed to read on/via given object.
	/// </summary>
	SL_STATUS_OBJECT_READ = 0x0036,
	/// <summary>
	/// Failed to write on/via given object.
	/// </summary>
	SL_STATUS_OBJECT_WRITE = 0x0037,
	/// <summary>
	/// Message is too long.
	/// </summary>
	SL_STATUS_MESSAGE_TOO_LONG = 0x0038,
	/// <summary>
	/// EEPROM MFG version mismatch.
	/// </summary>
	SL_STATUS_EEPROM_MFG_VERSION_MISMATCH = 0x0039,
	/// <summary>
	/// EEPROM Stack version mismatch.
	/// </summary>
	SL_STATUS_EEPROM_STACK_VERSION_MISMATCH = 0x003A,
	/// <summary>
	/// Flash write is inhibited.
	/// </summary>
	SL_STATUS_FLASH_WRITE_INHIBITED = 0x003B,
	/// <summary>
	/// Flash verification failed.
	/// </summary>
	SL_STATUS_FLASH_VERIFY_FAILED = 0x003C,
	/// <summary>
	/// Flash programming failed.
	/// </summary>
	SL_STATUS_FLASH_PROGRAM_FAILED = 0x003D,
	/// <summary>
	/// Flash erase failed.
	/// </summary>
	SL_STATUS_FLASH_ERASE_FAILED = 0x003E,
	/// <summary>
	/// MAC no data.
	/// </summary>
	SL_STATUS_MAC_NO_DATA = 0x003F,
	/// <summary>
	/// MAC no ACK received.
	/// </summary>
	SL_STATUS_MAC_NO_ACK_RECEIVED = 0x0040,
	/// <summary>
	/// MAC indirect timeout.
	/// </summary>
	SL_STATUS_MAC_INDIRECT_TIMEOUT = 0x0041,
	/// <summary>
	/// MAC unknown header type.
	/// </summary>
	SL_STATUS_MAC_UNKNOWN_HEADER_TYPE = 0x0042,
	/// <summary>
	/// MAC ACK unknown header type.
	/// </summary>
	SL_STATUS_MAC_ACK_HEADER_TYPE = 0x0043,
	/// <summary>
	/// MAC command transmit failure.
	/// </summary>
	SL_STATUS_MAC_COMMAND_TRANSMIT_FAILURE = 0x0044,
	/// <summary>
	/// Error in open NVM
	/// </summary>
	SL_STATUS_CLI_STORAGE_NVM_OPEN_ERROR = 0x0045,
	/// <summary>
	/// Image checksum is not valid.
	/// </summary>
	SL_STATUS_SECURITY_IMAGE_CHECKSUM_ERROR = 0x0046,
	/// <summary>
	/// Decryption failed
	/// </summary>
	SL_STATUS_SECURITY_DECRYPT_ERROR = 0x0047,
	/// <summary>
	/// Command was not recognized
	/// </summary>
	SL_STATUS_COMMAND_IS_INVALID = 0x0048,
	/// <summary>
	/// Command or parameter maximum length exceeded
	/// </summary>
	SL_STATUS_COMMAND_TOO_LONG = 0x0049,
	/// <summary>
	/// Data received does not form a complete command
	/// </summary>
	SL_STATUS_COMMAND_INCOMPLETE = 0x004A,
	/// <summary>
	/// Bus error, e.g. invalid DMA address
	/// </summary>
	SL_STATUS_BUS_ERROR = 0x004B,
	/// <summary>
	/// CCA failure.
	/// </summary>
	SL_STATUS_CCA_FAILURE = 0x004C,
	/// <summary>
	/// MAC scanning.
	/// </summary>
	SL_STATUS_MAC_SCANNING = 0x004D,
	/// <summary>
	/// MAC incorrect scan type.
	/// </summary>
	SL_STATUS_MAC_INCORRECT_SCAN_TYPE = 0x004E,
	/// <summary>
	/// Invalid channel mask.
	/// </summary>
	SL_STATUS_INVALID_CHANNEL_MASK = 0x004F,
	/// <summary>
	/// Bad scan duration.
	/// </summary>
	SL_STATUS_BAD_SCAN_DURATION = 0x0050,
	/// <summary>
	/// The MAC transmit queue is full
	/// </summary>
	SL_STATUS_MAC_TRANSMIT_QUEUE_FULL = 0x0053,
	/// <summary>
	/// The transmit attempt failed because the radio scheduler could not find a slot to transmit this packet in or a higher priority event interrupted it
	/// </summary>
	SL_STATUS_TRANSMIT_SCHEDULER_FAIL = 0x0054,
	/// <summary>
	/// An unsupported channel setting was specified
	/// </summary>
	SL_STATUS_TRANSMIT_INVALID_CHANNEL = 0x0055,
	/// <summary>
	/// An unsupported power setting was specified
	/// </summary>
	SL_STATUS_TRANSMIT_INVALID_POWER = 0x0056,
	/// <summary>
	/// The expected ACK was received after the last transmission
	/// </summary>
	SL_STATUS_TRANSMIT_ACK_RECEIVED = 0x0057,
	/// <summary>
	/// The transmit attempt was blocked from going over the air. Typically this is due to the Radio Hold Off (RHO) or Coexistence plugins as they can prevent transmits based on external signals.
	/// </summary>
	SL_STATUS_TRANSMIT_BLOCKED = 0x0058,
	/// <summary>
	/// The initialization was aborted as the NVM3 instance is not aligned properly in memory
	/// </summary>
	SL_STATUS_NVM3_ALIGNMENT_INVALID = 0x0059,
	/// <summary>
	/// The initialization was aborted as the size of the NVM3 instance is too small
	/// </summary>
	SL_STATUS_NVM3_SIZE_TOO_SMALL = 0x005A,
	/// <summary>
	/// The initialization was aborted as the NVM3 page size is not supported
	/// </summary>
	SL_STATUS_NVM3_PAGE_SIZE_NOT_SUPPORTED = 0x005B,
	/// <summary>
	/// The application that there was an error initializing some of the tokens
	/// </summary>
	SL_STATUS_NVM3_TOKEN_INIT_FAILED = 0x005C,
	/// <summary>
	/// The initialization was aborted as the NVM3 instance was already opened with other parameters
	/// </summary>
	SL_STATUS_NVM3_OPENED_WITH_OTHER_PARAMETERS = 0x005D,
	/// <summary>
	/// Initialization aborted, no valid page found
	/// </summary>
	SL_STATUS_NVM3_NO_VALID_PAGES = 0x005E,
	/// <summary>
	/// The object size is not supported
	/// </summary>
	SL_STATUS_NVM3_OBJECT_SIZE_NOT_SUPPORTED = 0x005F,
	/// <summary>
	/// Trying to access a data object which is currently a counter object
	/// </summary>
	SL_STATUS_NVM3_OBJECT_IS_NOT_DATA = 0x0060,
	/// <summary>
	/// Trying to access a counter object which is currently a data object
	/// </summary>
	SL_STATUS_NVM3_OBJECT_IS_NOT_A_COUNTER = 0x0061,
	/// <summary>
	/// The object is too large
	/// </summary>
	SL_STATUS_NVM3_WRITE_DATA_SIZE = 0x0062,
	/// <summary>
	/// Trying to read with a length different from actual object size
	/// </summary>
	SL_STATUS_NVM3_READ_DATA_SIZE = 0x0063,
	/// <summary>
	/// The module was opened with a full NVM
	/// </summary>
	SL_STATUS_NVM3_INIT_WITH_FULL_NVM = 0x0064,
	/// <summary>
	/// Illegal parameter
	/// </summary>
	SL_STATUS_NVM3_RESIZE_PARAMETER = 0x0065,
	/// <summary>
	/// Not enough NVM to complete resize
	/// </summary>
	SL_STATUS_NVM3_RESIZE_NOT_ENOUGH_SPACE = 0x0066,
	/// <summary>
	/// Erase counts are not valid
	/// </summary>
	SL_STATUS_NVM3_ERASE_COUNT_ERROR = 0x0067,
	/// <summary>
	/// A NVM function call was failing
	/// </summary>
	SL_STATUS_NVM3_NVM_ACCESS = 0x0068,
	/// <summary>
	/// Write to memory that is not erased
	/// </summary>
	SL_STATUS_NVM3_WRITE_TO_NOT_ERASED = 0x006D,
	/// <summary>
	/// Invalid NVM address
	/// </summary>
	SL_STATUS_NVM3_INVALID_ADDR = 0x006E,
	/// <summary>
	/// Key validation failure
	/// </summary>
	SL_STATUS_NVM3_KEY_MISMATCH = 0x006F,
	/// <summary>
	/// Size mismatch error
	/// </summary>
	SL_STATUS_NVM3_SIZE_ERROR = 0x0070,
	/// <summary>
	/// Emulator error
	/// </summary>
	SL_STATUS_NVM3_EMULATOR = 0x0071,
	/// <summary>
	/// Encryption failed
	/// </summary>
	SL_STATUS_SECURITY_ENCRYPT_ERROR = 0x0072,
	/// <summary>
	/// Error in obtaining crypto key
	/// </summary>
	SL_STATUS_SECURITY_KEY_ERROR = 0x0073,
	/// <summary>
	/// Error in obtaining random number
	/// </summary>
	SL_STATUS_SECURITY_RANDOM_NUM_GEN_ERROR = 0x0074,
	/// <summary>
	/// Bonding procedure can&apos;t be started because device has no space left for bond.
	/// </summary>
	SL_STATUS_BT_OUT_OF_BONDS = 0x0402,
	/// <summary>
	/// Unspecified error
	/// </summary>
	SL_STATUS_BT_UNSPECIFIED = 0x0403,
	/// <summary>
	/// Hardware failure
	/// </summary>
	SL_STATUS_BT_HARDWARE = 0x0404,
	/// <summary>
	/// The bonding does not exist.
	/// </summary>
	SL_STATUS_BT_NO_BONDING = 0x0406,
	/// <summary>
	/// Error using crypto functions
	/// </summary>
	SL_STATUS_BT_CRYPTO = 0x0407,
	/// <summary>
	/// Data was corrupted.
	/// </summary>
	SL_STATUS_BT_DATA_CORRUPTED = 0x0408,
	/// <summary>
	/// Invalid periodic advertising sync handle
	/// </summary>
	SL_STATUS_BT_INVALID_SYNC_HANDLE = 0x040A,
	/// <summary>
	/// Bluetooth cannot be used on this hardware
	/// </summary>
	SL_STATUS_BT_INVALID_MODULE_ACTION = 0x040B,
	/// <summary>
	/// Error received from radio
	/// </summary>
	SL_STATUS_BT_RADIO = 0x040C,
	/// <summary>
	/// Returned when remote disconnects the connection-oriented channel by sending disconnection request.
	/// </summary>
	SL_STATUS_BT_L2CAP_REMOTE_DISCONNECTED = 0x040D,
	/// <summary>
	/// Returned when local host disconnect the connection-oriented channel by sending disconnection request.
	/// </summary>
	SL_STATUS_BT_L2CAP_LOCAL_DISCONNECTED = 0x040E,
	/// <summary>
	/// Returned when local host did not find a connection-oriented channel with given destination CID.
	/// </summary>
	SL_STATUS_BT_L2CAP_CID_NOT_EXIST = 0x040F,
	/// <summary>
	/// Returned when connection-oriented channel disconnected due to LE connection is dropped.
	/// </summary>
	SL_STATUS_BT_L2CAP_LE_DISCONNECTED = 0x0410,
	/// <summary>
	/// Returned when connection-oriented channel disconnected due to remote end send data even without credit.
	/// </summary>
	SL_STATUS_BT_L2CAP_FLOW_CONTROL_VIOLATED = 0x0412,
	/// <summary>
	/// Returned when connection-oriented channel disconnected due to remote end send flow control credits exceed 65535.
	/// </summary>
	SL_STATUS_BT_L2CAP_FLOW_CONTROL_CREDIT_OVERFLOWED = 0x0413,
	/// <summary>
	/// Returned when connection-oriented channel has run out of flow control credit and local application still trying to send data.
	/// </summary>
	SL_STATUS_BT_L2CAP_NO_FLOW_CONTROL_CREDIT = 0x0414,
	/// <summary>
	/// Returned when connection-oriented channel has not received connection response message within maximum timeout.
	/// </summary>
	SL_STATUS_BT_L2CAP_CONNECTION_REQUEST_TIMEOUT = 0x0415,
	/// <summary>
	/// Returned when local host received a connection-oriented channel connection response with an invalid destination CID.
	/// </summary>
	SL_STATUS_BT_L2CAP_INVALID_CID = 0x0416,
	/// <summary>
	/// Returned when local host application tries to send a command which is not suitable for L2CAP channel&apos;s current state.
	/// </summary>
	SL_STATUS_BT_L2CAP_WRONG_STATE = 0x0417,
	/// <summary>
	/// Flash reserved for PS store is full
	/// </summary>
	SL_STATUS_BT_PS_STORE_FULL = 0x041B,
	/// <summary>
	/// PS key not found
	/// </summary>
	SL_STATUS_BT_PS_KEY_NOT_FOUND = 0x041C,
	/// <summary>
	/// Mismatched or insufficient security level
	/// </summary>
	SL_STATUS_BT_APPLICATION_MISMATCHED_OR_INSUFFICIENT_SECURITY = 0x041D,
	/// <summary>
	/// Encryption/decryption operation failed.
	/// </summary>
	SL_STATUS_BT_APPLICATION_ENCRYPTION_DECRYPTION_ERROR = 0x041E,
	/// <summary>
	/// Connection does not exist, or connection open request was cancelled.
	/// </summary>
	SL_STATUS_BT_CTRL_UNKNOWN_CONNECTION_IDENTIFIER = 0x1002,
	/// <summary>
	/// Pairing or authentication failed due to incorrect results in the pairing or authentication procedure. This could be due to an incorrect PIN or Link Key
	/// </summary>
	SL_STATUS_BT_CTRL_AUTHENTICATION_FAILURE = 0x1005,
	/// <summary>
	/// Pairing failed because of missing PIN, or authentication failed because of missing Key
	/// </summary>
	SL_STATUS_BT_CTRL_PIN_OR_KEY_MISSING = 0x1006,
	/// <summary>
	/// Controller is out of memory.
	/// </summary>
	SL_STATUS_BT_CTRL_MEMORY_CAPACITY_EXCEEDED = 0x1007,
	/// <summary>
	/// Link supervision timeout has expired.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_TIMEOUT = 0x1008,
	/// <summary>
	/// Controller is at limit of connections it can support.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_LIMIT_EXCEEDED = 0x1009,
	/// <summary>
	/// The Synchronous Connection Limit to a Device Exceeded error code indicates that the Controller has reached the limit to the number of synchronous connections that can be achieved to a device.
	/// </summary>
	SL_STATUS_BT_CTRL_SYNCHRONOUS_CONNECTION_LIMIT_EXCEEDED = 0x100A,
	/// <summary>
	/// The ACL Connection Already Exists error code indicates that an attempt to create a new ACL Connection to a device when there is already a connection to this device.
	/// </summary>
	SL_STATUS_BT_CTRL_ACL_CONNECTION_ALREADY_EXISTS = 0x100B,
	/// <summary>
	/// Command requested cannot be executed because the Controller is in a state where it cannot process this command at this time.
	/// </summary>
	SL_STATUS_BT_CTRL_COMMAND_DISALLOWED = 0x100C,
	/// <summary>
	/// The Connection Rejected Due To Limited Resources error code indicates that an incoming connection was rejected due to limited resources.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_REJECTED_DUE_TO_LIMITED_RESOURCES = 0x100D,
	/// <summary>
	/// The Connection Rejected Due To Security Reasons error code indicates that a connection was rejected due to security requirements not being fulfilled, like authentication or pairing.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_REJECTED_DUE_TO_SECURITY_REASONS = 0x100E,
	/// <summary>
	/// The Connection was rejected because this device does not accept the BD_ADDR. This may be because the device will only accept connections from specific BD_ADDRs.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_REJECTED_DUE_TO_UNACCEPTABLE_BD_ADDR = 0x100F,
	/// <summary>
	/// The Connection Accept Timeout has been exceeded for this connection attempt.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_ACCEPT_TIMEOUT_EXCEEDED = 0x1010,
	/// <summary>
	/// A feature or parameter value in the HCI command is not supported.
	/// </summary>
	SL_STATUS_BT_CTRL_UNSUPPORTED_FEATURE_OR_PARAMETER_VALUE = 0x1011,
	/// <summary>
	/// Command contained invalid parameters.
	/// </summary>
	SL_STATUS_BT_CTRL_INVALID_COMMAND_PARAMETERS = 0x1012,
	/// <summary>
	/// User on the remote device terminated the connection.
	/// </summary>
	SL_STATUS_BT_CTRL_REMOTE_USER_TERMINATED = 0x1013,
	/// <summary>
	/// The remote device terminated the connection because of low resources
	/// </summary>
	SL_STATUS_BT_CTRL_REMOTE_DEVICE_TERMINATED_CONNECTION_DUE_TO_LOW_RESOURCES = 0x1014,
	/// <summary>
	/// Remote Device Terminated Connection due to Power Off
	/// </summary>
	SL_STATUS_BT_CTRL_REMOTE_POWERING_OFF = 0x1015,
	/// <summary>
	/// Local device terminated the connection.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_TERMINATED_BY_LOCAL_HOST = 0x1016,
	/// <summary>
	/// The Controller is disallowing an authentication or pairing procedure because too little time has elapsed since the last authentication or pairing attempt failed.
	/// </summary>
	SL_STATUS_BT_CTRL_REPEATED_ATTEMPTS = 0x1017,
	/// <summary>
	/// The device does not allow pairing. This can be for example, when a device only allows pairing during a certain time window after some user input allows pairing
	/// </summary>
	SL_STATUS_BT_CTRL_PAIRING_NOT_ALLOWED = 0x1018,
	/// <summary>
	/// The remote device does not support the feature associated with the issued command.
	/// </summary>
	SL_STATUS_BT_CTRL_UNSUPPORTED_REMOTE_FEATURE = 0x101A,
	/// <summary>
	/// Indicates that some LMP PDU / LL Control PDU parameters were invalid
	/// </summary>
	SL_STATUS_BT_CTRL_INVALID_LL_PARAMETERS = 0x101E,
	/// <summary>
	/// No other error code specified is appropriate to use.
	/// </summary>
	SL_STATUS_BT_CTRL_UNSPECIFIED_ERROR = 0x101F,
	/// <summary>
	/// Connection terminated due to link-layer procedure timeout.
	/// </summary>
	SL_STATUS_BT_CTRL_LL_RESPONSE_TIMEOUT = 0x1022,
	/// <summary>
	/// LL procedure has collided with the same transaction or procedure that is already in progress.
	/// </summary>
	SL_STATUS_BT_CTRL_LL_PROCEDURE_COLLISION = 0x1023,
	/// <summary>
	/// The requested encryption mode is not acceptable at this time.
	/// </summary>
	SL_STATUS_BT_CTRL_ENCRYPTION_MODE_NOT_ACCEPTABLE = 0x1025,
	/// <summary>
	/// Link key cannot be changed because a fixed unit key is being used.
	/// </summary>
	SL_STATUS_BT_CTRL_LINK_KEY_CANNOT_BE_CHANGED = 0x1026,
	/// <summary>
	/// LMP PDU or LL PDU that includes an instant cannot be performed because the instant when this would have occurred has passed.
	/// </summary>
	SL_STATUS_BT_CTRL_INSTANT_PASSED = 0x1028,
	/// <summary>
	/// It was not possible to pair as a unit key was requested and it is not supported.
	/// </summary>
	SL_STATUS_BT_CTRL_PAIRING_WITH_UNIT_KEY_NOT_SUPPORTED = 0x1029,
	/// <summary>
	/// LMP transaction was started that collides with an ongoing transaction.
	/// </summary>
	SL_STATUS_BT_CTRL_DIFFERENT_TRANSACTION_COLLISION = 0x102A,
	/// <summary>
	/// The Controller cannot perform channel assessment because it is not supported.
	/// </summary>
	SL_STATUS_BT_CTRL_CHANNEL_ASSESSMENT_NOT_SUPPORTED = 0x102E,
	/// <summary>
	/// The HCI command or LMP PDU sent is only possible on an encrypted link.
	/// </summary>
	SL_STATUS_BT_CTRL_INSUFFICIENT_SECURITY = 0x102F,
	/// <summary>
	/// A parameter value requested is outside the mandatory range of parameters for the given HCI command or LMP PDU.
	/// </summary>
	SL_STATUS_BT_CTRL_PARAMETER_OUT_OF_MANDATORY_RANGE = 0x1030,
	/// <summary>
	/// The IO capabilities request or response was rejected because the sending Host does not support Secure Simple Pairing even though the receiving Link Manager does.
	/// </summary>
	SL_STATUS_BT_CTRL_SIMPLE_PAIRING_NOT_SUPPORTED_BY_HOST = 0x1037,
	/// <summary>
	/// The Host is busy with another pairing operation and unable to support the requested pairing. The receiving device should retry pairing again later.
	/// </summary>
	SL_STATUS_BT_CTRL_HOST_BUSY_PAIRING = 0x1038,
	/// <summary>
	/// The Controller could not calculate an appropriate value for the Channel selection operation.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_REJECTED_DUE_TO_NO_SUITABLE_CHANNEL_FOUND = 0x1039,
	/// <summary>
	/// Operation was rejected because the controller is busy and unable to process the request.
	/// </summary>
	SL_STATUS_BT_CTRL_CONTROLLER_BUSY = 0x103A,
	/// <summary>
	/// Remote device terminated the connection because of an unacceptable connection interval.
	/// </summary>
	SL_STATUS_BT_CTRL_UNACCEPTABLE_CONNECTION_INTERVAL = 0x103B,
	/// <summary>
	/// Advertising for a fixed duration completed or, for directed advertising, that advertising completed without a connection being created.
	/// </summary>
	SL_STATUS_BT_CTRL_ADVERTISING_TIMEOUT = 0x103C,
	/// <summary>
	/// Connection was terminated because the Message Integrity Check (MIC) failed on a received packet.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_TERMINATED_DUE_TO_MIC_FAILURE = 0x103D,
	/// <summary>
	/// LL initiated a connection but the connection has failed to be established. Controller did not receive any packets from remote end.
	/// </summary>
	SL_STATUS_BT_CTRL_CONNECTION_FAILED_TO_BE_ESTABLISHED = 0x103E,
	/// <summary>
	/// The MAC of the 802.11 AMP was requested to connect to a peer, but the connection failed.
	/// </summary>
	SL_STATUS_BT_CTRL_MAC_CONNECTION_FAILED = 0x103F,
	/// <summary>
	/// The master, at this time, is unable to make a coarse adjustment to the piconet clock, using the supplied parameters. Instead the master will attempt to move the clock using clock dragging.
	/// </summary>
	SL_STATUS_BT_CTRL_COARSE_CLOCK_ADJUSTMENT_REJECTED_BUT_WILL_TRY_TO_ADJUST_USING_CLOCK_DRAGGING = 0x1040,
	/// <summary>
	/// A command was sent from the Host that should identify an Advertising or Sync handle, but the Advertising or Sync handle does not exist.
	/// </summary>
	SL_STATUS_BT_CTRL_UNKNOWN_ADVERTISING_IDENTIFIER = 0x1042,
	/// <summary>
	/// Number of operations requested has been reached and has indicated the completion of the activity (e.g., advertising or scanning).
	/// </summary>
	SL_STATUS_BT_CTRL_LIMIT_REACHED = 0x1043,
	/// <summary>
	/// A request to the Controller issued by the Host and still pending was successfully canceled.
	/// </summary>
	SL_STATUS_BT_CTRL_OPERATION_CANCELLED_BY_HOST = 0x1044,
	/// <summary>
	/// An attempt was made to send or receive a packet that exceeds the maximum allowed packet length.
	/// </summary>
	SL_STATUS_BT_CTRL_PACKET_TOO_LONG = 0x1045,
	/// <summary>
	/// Information was provided too late to the controller.
	/// </summary>
	SL_STATUS_BT_CTRL_TOO_LATE = 0x1046,
	/// <summary>
	/// Information was provided too early to the controller.
	/// </summary>
	SL_STATUS_BT_CTRL_TOO_EARLY = 0x1047,
	/// <summary>
	/// Indicates that the result of the requested operation would yield too few physical channels.
	/// </summary>
	SL_STATUS_BT_CTRL_INSUFFICIENT_CHANNELS = 0x1048,
	/// <summary>
	/// The attribute handle given was not valid on this server
	/// </summary>
	SL_STATUS_BT_ATT_INVALID_HANDLE = 0x1101,
	/// <summary>
	/// The attribute cannot be read
	/// </summary>
	SL_STATUS_BT_ATT_READ_NOT_PERMITTED = 0x1102,
	/// <summary>
	/// The attribute cannot be written
	/// </summary>
	SL_STATUS_BT_ATT_WRITE_NOT_PERMITTED = 0x1103,
	/// <summary>
	/// The attribute PDU was invalid
	/// </summary>
	SL_STATUS_BT_ATT_INVALID_PDU = 0x1104,
	/// <summary>
	/// The attribute requires authentication before it can be read or written.
	/// </summary>
	SL_STATUS_BT_ATT_INSUFFICIENT_AUTHENTICATION = 0x1105,
	/// <summary>
	/// Attribute Server does not support the request received from the client.
	/// </summary>
	SL_STATUS_BT_ATT_REQUEST_NOT_SUPPORTED = 0x1106,
	/// <summary>
	/// Offset specified was past the end of the attribute
	/// </summary>
	SL_STATUS_BT_ATT_INVALID_OFFSET = 0x1107,
	/// <summary>
	/// The attribute requires authorization before it can be read or written.
	/// </summary>
	SL_STATUS_BT_ATT_INSUFFICIENT_AUTHORIZATION = 0x1108,
	/// <summary>
	/// Too many prepare writes have been queued
	/// </summary>
	SL_STATUS_BT_ATT_PREPARE_QUEUE_FULL = 0x1109,
	/// <summary>
	/// No attribute found within the given attribute handle range.
	/// </summary>
	SL_STATUS_BT_ATT_ATT_NOT_FOUND = 0x110A,
	/// <summary>
	/// The attribute cannot be read or written using the Read Blob Request
	/// </summary>
	SL_STATUS_BT_ATT_ATT_NOT_LONG = 0x110B,
	/// <summary>
	/// The Encryption Key Size used for encrypting this link is insufficient.
	/// </summary>
	SL_STATUS_BT_ATT_INSUFFICIENT_ENC_KEY_SIZE = 0x110C,
	/// <summary>
	/// The attribute value length is invalid for the operation
	/// </summary>
	SL_STATUS_BT_ATT_INVALID_ATT_LENGTH = 0x110D,
	/// <summary>
	/// The attribute request that was requested has encountered an error that was unlikely, and therefore could not be completed as requested.
	/// </summary>
	SL_STATUS_BT_ATT_UNLIKELY_ERROR = 0x110E,
	/// <summary>
	/// The attribute requires encryption before it can be read or written.
	/// </summary>
	SL_STATUS_BT_ATT_INSUFFICIENT_ENCRYPTION = 0x110F,
	/// <summary>
	/// The attribute type is not a supported grouping attribute as defined by a higher layer specification.
	/// </summary>
	SL_STATUS_BT_ATT_UNSUPPORTED_GROUP_TYPE = 0x1110,
	/// <summary>
	/// Insufficient Resources to complete the request
	/// </summary>
	SL_STATUS_BT_ATT_INSUFFICIENT_RESOURCES = 0x1111,
	/// <summary>
	/// The server requests the client to rediscover the database.
	/// </summary>
	SL_STATUS_BT_ATT_OUT_OF_SYNC = 0x1112,
	/// <summary>
	/// The attribute parameter value was not allowed.
	/// </summary>
	SL_STATUS_BT_ATT_VALUE_NOT_ALLOWED = 0x1113,
	/// <summary>
	/// When this is returned in a BGAPI response, the application tried to read or write the value of a user attribute from the GATT database.
	/// </summary>
	SL_STATUS_BT_ATT_APPLICATION = 0x1180,
	/// <summary>
	/// The requested write operation cannot be fulfilled for reasons other than permissions.
	/// </summary>
	SL_STATUS_BT_ATT_WRITE_REQUEST_REJECTED = 0x11FC,
	/// <summary>
	/// The Client Characteristic Configuration descriptor is not configured according to the requirements of the profile or service.
	/// </summary>
	SL_STATUS_BT_ATT_CLIENT_CHARACTERISTIC_CONFIGURATION_DESCRIPTOR_IMPROPERLY_CONFIGURED = 0x11FD,
	/// <summary>
	/// The profile or service request cannot be serviced because an operation that has been previously triggered is still in progress.
	/// </summary>
	SL_STATUS_BT_ATT_PROCEDURE_ALREADY_IN_PROGRESS = 0x11FE,
	/// <summary>
	/// The attribute value is out of range as defined by a profile or service specification.
	/// </summary>
	SL_STATUS_BT_ATT_OUT_OF_RANGE = 0x11FF,
	/// <summary>
	/// The user input of passkey failed, for example, the user cancelled the operation
	/// </summary>
	SL_STATUS_BT_SMP_PASSKEY_ENTRY_FAILED = 0x1201,
	/// <summary>
	/// Out of Band data is not available for authentication
	/// </summary>
	SL_STATUS_BT_SMP_OOB_NOT_AVAILABLE = 0x1202,
	/// <summary>
	/// The pairing procedure cannot be performed as authentication requirements cannot be met due to IO capabilities of one or both devices
	/// </summary>
	SL_STATUS_BT_SMP_AUTHENTICATION_REQUIREMENTS = 0x1203,
	/// <summary>
	/// The confirm value does not match the calculated compare value
	/// </summary>
	SL_STATUS_BT_SMP_CONFIRM_VALUE_FAILED = 0x1204,
	/// <summary>
	/// Pairing is not supported by the device
	/// </summary>
	SL_STATUS_BT_SMP_PAIRING_NOT_SUPPORTED = 0x1205,
	/// <summary>
	/// The resultant encryption key size is insufficient for the security requirements of this device
	/// </summary>
	SL_STATUS_BT_SMP_ENCRYPTION_KEY_SIZE = 0x1206,
	/// <summary>
	/// The SMP command received is not supported on this device
	/// </summary>
	SL_STATUS_BT_SMP_COMMAND_NOT_SUPPORTED = 0x1207,
	/// <summary>
	/// Pairing failed due to an unspecified reason
	/// </summary>
	SL_STATUS_BT_SMP_UNSPECIFIED_REASON = 0x1208,
	/// <summary>
	/// Pairing or authentication procedure is disallowed because too little time has elapsed since last pairing request or security request
	/// </summary>
	SL_STATUS_BT_SMP_REPEATED_ATTEMPTS = 0x1209,
	/// <summary>
	/// The Invalid Parameters error code indicates: the command length is invalid or a parameter is outside of the specified range.
	/// </summary>
	SL_STATUS_BT_SMP_INVALID_PARAMETERS = 0x120A,
	/// <summary>
	/// Indicates to the remote device that the DHKey Check value received doesn&apos;t match the one calculated by the local device.
	/// </summary>
	SL_STATUS_BT_SMP_DHKEY_CHECK_FAILED = 0x120B,
	/// <summary>
	/// Indicates that the confirm values in the numeric comparison protocol do not match.
	/// </summary>
	SL_STATUS_BT_SMP_NUMERIC_COMPARISON_FAILED = 0x120C,
	/// <summary>
	/// Indicates that the pairing over the LE transport failed due to a Pairing Request sent over the BR/EDR transport in process.
	/// </summary>
	SL_STATUS_BT_SMP_BREDR_PAIRING_IN_PROGRESS = 0x120D,
	/// <summary>
	/// Indicates that the BR/EDR Link Key generated on the BR/EDR transport cannot be used to derive and distribute keys for the LE transport.
	/// </summary>
	SL_STATUS_BT_SMP_CROSS_TRANSPORT_KEY_DERIVATION_GENERATION_NOT_ALLOWED = 0x120E,
	/// <summary>
	/// Indicates that the device chose not to accept a distributed key.
	/// </summary>
	SL_STATUS_BT_SMP_KEY_REJECTED = 0x120F,
	/// <summary>
	/// Returned when trying to add a key or some other unique resource with an ID which already exists
	/// </summary>
	SL_STATUS_BT_MESH_ALREADY_EXISTS = 0x0501,
	/// <summary>
	/// Returned when trying to manipulate a key or some other resource with an ID which does not exist
	/// </summary>
	SL_STATUS_BT_MESH_DOES_NOT_EXIST = 0x0502,
	/// <summary>
	/// Returned when an operation cannot be executed because a pre-configured limit for keys, key bindings, elements, models, virtual addresses, provisioned devices, or provisioning sessions is reached
	/// </summary>
	SL_STATUS_BT_MESH_LIMIT_REACHED = 0x0503,
	/// <summary>
	/// Returned when trying to use a reserved address or add a &quot;pre-provisioned&quot; device using an address already used by some other device
	/// </summary>
	SL_STATUS_BT_MESH_INVALID_ADDRESS = 0x0504,
	/// <summary>
	/// In a BGAPI response, the user supplied malformed data; in a BGAPI event, the remote end responded with malformed or unrecognized data
	/// </summary>
	SL_STATUS_BT_MESH_MALFORMED_DATA = 0x0505,
	/// <summary>
	/// An attempt was made to initialize a subsystem that was already initialized.
	/// </summary>
	SL_STATUS_BT_MESH_ALREADY_INITIALIZED = 0x0506,
	/// <summary>
	/// An attempt was made to use a subsystem that wasn&apos;t initialized yet. Call the subsystem&apos;s init function first.
	/// </summary>
	SL_STATUS_BT_MESH_NOT_INITIALIZED = 0x0507,
	/// <summary>
	/// Returned when trying to establish a friendship as a Low Power Node, but no acceptable friend offer message was received.
	/// </summary>
	SL_STATUS_BT_MESH_NO_FRIEND_OFFER = 0x0508,
	/// <summary>
	/// Provisioning link was unexpectedly closed before provisioning was complete.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_LINK_CLOSED = 0x0509,
	/// <summary>
	/// An unrecognized provisioning PDU was received.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_INVALID_PDU = 0x050A,
	/// <summary>
	/// A provisioning PDU with wrong length or containing field values that are out of bounds was received.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_INVALID_PDU_FORMAT = 0x050B,
	/// <summary>
	/// An unexpected (out of sequence) provisioning PDU was received.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_UNEXPECTED_PDU = 0x050C,
	/// <summary>
	/// The computed confirmation value did not match the expected value.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_CONFIRMATION_FAILED = 0x050D,
	/// <summary>
	/// Provisioning could not be continued due to insufficient resources.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_OUT_OF_RESOURCES = 0x050E,
	/// <summary>
	/// The provisioning data block could not be decrypted.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_DECRYPTION_FAILED = 0x050F,
	/// <summary>
	/// An unexpected error happened during provisioning.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_UNEXPECTED_ERROR = 0x0510,
	/// <summary>
	/// Device could not assign unicast addresses to all of its elements.
	/// </summary>
	SL_STATUS_BT_MESH_PROV_CANNOT_ASSIGN_ADDR = 0x0511,
	/// <summary>
	/// Returned when trying to reuse an address of a previously deleted device before an IV Index Update has been executed.
	/// </summary>
	SL_STATUS_BT_MESH_ADDRESS_TEMPORARILY_UNAVAILABLE = 0x0512,
	/// <summary>
	/// Returned when trying to assign an address that is used by one of the devices in the Device Database, or by the Provisioner itself.
	/// </summary>
	SL_STATUS_BT_MESH_ADDRESS_ALREADY_USED = 0x0513,
	/// <summary>
	/// Application key or publish address are not set
	/// </summary>
	SL_STATUS_BT_MESH_PUBLISH_NOT_CONFIGURED = 0x0514,
	/// <summary>
	/// Application key is not bound to a model
	/// </summary>
	SL_STATUS_BT_MESH_APP_KEY_NOT_BOUND = 0x0515,
	/// <summary>
	/// Returned when address in request was not valid
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_ADDRESS = 0x1301,
	/// <summary>
	/// Returned when model identified is not found for a given element
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_MODEL = 0x1302,
	/// <summary>
	/// Returned when the key identified by AppKeyIndex is not stored in the node
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_APP_KEY = 0x1303,
	/// <summary>
	/// Returned when the key identified by NetKeyIndex is not stored in the node
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_NET_KEY = 0x1304,
	/// <summary>
	/// Returned when The node cannot serve the request due to insufficient resources
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INSUFFICIENT_RESOURCES = 0x1305,
	/// <summary>
	/// Returned when the key identified is already stored in the node and the new NetKey value is different
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_KEY_INDEX_EXISTS = 0x1306,
	/// <summary>
	/// Returned when the model does not support the publish mechanism
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_PUBLISH_PARAMS = 0x1307,
	/// <summary>
	/// Returned when  the model does not support the subscribe mechanism
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_NOT_SUBSCRIBE_MODEL = 0x1308,
	/// <summary>
	/// Returned when storing of the requested parameters failed
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_STORAGE_FAILURE = 0x1309,
	/// <summary>
	/// Returned when requested setting is not supported
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_NOT_SUPPORTED = 0x130A,
	/// <summary>
	/// Returned when the requested update operation cannot be performed due to general constraints
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_CANNOT_UPDATE = 0x130B,
	/// <summary>
	/// Returned when the requested delete operation cannot be performed due to general constraints
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_CANNOT_REMOVE = 0x130C,
	/// <summary>
	/// Returned when the requested bind operation cannot be performed due to general constraints
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_CANNOT_BIND = 0x130D,
	/// <summary>
	/// Returned when The node cannot start advertising with Node Identity or Proxy since the maximum number of parallel advertising is reached
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_TEMPORARILY_UNABLE = 0x130E,
	/// <summary>
	/// Returned when the requested state cannot be set
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_CANNOT_SET = 0x130F,
	/// <summary>
	/// Returned when an unspecified error took place
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_UNSPECIFIED = 0x1310,
	/// <summary>
	/// Returned when the NetKeyIndex and AppKeyIndex combination is not valid for a Config AppKey Update
	/// </summary>
	SL_STATUS_BT_MESH_FOUNDATION_INVALID_BINDING = 0x1311,
	/// <summary>
	/// Invalid firmware keyset
	/// </summary>
	SL_STATUS_WIFI_INVALID_KEY = 0x0B01,
	/// <summary>
	/// The firmware download took too long
	/// </summary>
	SL_STATUS_WIFI_FIRMWARE_DOWNLOAD_TIMEOUT = 0x0B02,
	/// <summary>
	/// Unknown request ID or wrong interface ID used
	/// </summary>
	SL_STATUS_WIFI_UNSUPPORTED_MESSAGE_ID = 0x0B03,
	/// <summary>
	/// The request is successful but some parameters have been ignored
	/// </summary>
	SL_STATUS_WIFI_WARNING = 0x0B04,
	/// <summary>
	/// No Packets waiting to be received
	/// </summary>
	SL_STATUS_WIFI_NO_PACKET_TO_RECEIVE = 0x0B05,
	/// <summary>
	/// The sleep mode is granted
	/// </summary>
	SL_STATUS_WIFI_SLEEP_GRANTED = 0x0B08,
	/// <summary>
	/// The WFx does not go back to sleep
	/// </summary>
	SL_STATUS_WIFI_SLEEP_NOT_GRANTED = 0x0B09,
	/// <summary>
	/// The SecureLink MAC key was not found
	/// </summary>
	SL_STATUS_WIFI_SECURE_LINK_MAC_KEY_ERROR = 0x0B10,
	/// <summary>
	/// The SecureLink MAC key is already installed in OTP
	/// </summary>
	SL_STATUS_WIFI_SECURE_LINK_MAC_KEY_ALREADY_BURNED = 0x0B11,
	/// <summary>
	/// The SecureLink MAC key cannot be installed in RAM
	/// </summary>
	SL_STATUS_WIFI_SECURE_LINK_RAM_MODE_NOT_ALLOWED = 0x0B12,
	/// <summary>
	/// The SecureLink MAC key installation failed
	/// </summary>
	SL_STATUS_WIFI_SECURE_LINK_FAILED_UNKNOWN_MODE = 0x0B13,
	/// <summary>
	/// SecureLink key (re)negotiation failed
	/// </summary>
	SL_STATUS_WIFI_SECURE_LINK_EXCHANGE_FAILED = 0x0B14,
	/// <summary>
	/// The device is in an inappropriate state to perform the request
	/// </summary>
	SL_STATUS_WIFI_WRONG_STATE = 0x0B18,
	/// <summary>
	/// The request failed due to regulatory limitations
	/// </summary>
	SL_STATUS_WIFI_CHANNEL_NOT_ALLOWED = 0x0B19,
	/// <summary>
	/// The connection request failed because no suitable AP was found
	/// </summary>
	SL_STATUS_WIFI_NO_MATCHING_AP = 0x0B1A,
	/// <summary>
	/// The connection request was aborted by host
	/// </summary>
	SL_STATUS_WIFI_CONNECTION_ABORTED = 0x0B1B,
	/// <summary>
	/// The connection request failed because of a timeout
	/// </summary>
	SL_STATUS_WIFI_CONNECTION_TIMEOUT = 0x0B1C,
	/// <summary>
	/// The connection request failed because the AP rejected the device
	/// </summary>
	SL_STATUS_WIFI_CONNECTION_REJECTED_BY_AP = 0x0B1D,
	/// <summary>
	/// The connection request failed because the WPA handshake did not complete successfully
	/// </summary>
	SL_STATUS_WIFI_CONNECTION_AUTH_FAILURE = 0x0B1E,
	/// <summary>
	/// The request failed because the retry limit was exceeded
	/// </summary>
	SL_STATUS_WIFI_RETRY_EXCEEDED = 0x0B1F,
	/// <summary>
	/// The request failed because the MSDU life time was exceeded
	/// </summary>
	SL_STATUS_WIFI_TX_LIFETIME_EXCEEDED = 0x0B20,
	/// <summary>
	/// Critical fault
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_FAULT = 0x1501,
	/// <summary>
	/// ALU operation output NaN
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_ALU_NAN = 0x1502,
	/// <summary>
	/// ALU numeric overflow
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_ALU_OVERFLOW = 0x1503,
	/// <summary>
	/// ALU numeric underflow
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_ALU_UNDERFLOW = 0x1504,
	/// <summary>
	/// Overflow during array store
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_STORE_CONVERSION_OVERFLOW = 0x1505,
	/// <summary>
	/// Underflow during array store conversion
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_STORE_CONVERSION_UNDERFLOW = 0x1506,
	/// <summary>
	/// Infinity encountered during array store conversion
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_STORE_CONVERSION_INFINITY = 0x1507,
	/// <summary>
	/// NaN encountered during array store conversion
	/// </summary>
	SL_STATUS_COMPUTE_DRIVER_STORE_CONVERSION_NAN = 0x1508,
	/// <summary>
	/// MATH NaN encountered
	/// </summary>
	SL_STATUS_COMPUTE_MATH_NAN = 0x1512,
	/// <summary>
	/// MATH Infinity encountered
	/// </summary>
	SL_STATUS_COMPUTE_MATH_INFINITY = 0x1513,
	/// <summary>
	/// MATH numeric overflow
	/// </summary>
	SL_STATUS_COMPUTE_MATH_OVERFLOW = 0x1514,
	/// <summary>
	/// MATH numeric underflow
	/// </summary>
	SL_STATUS_COMPUTE_MATH_UNDERFLOW = 0x1515,
	/// <summary>
	/// Packet is dropped by packet-handoff callbacks
	/// </summary>
	SL_STATUS_ZIGBEE_PACKET_HANDOFF_DROPPED = 0x0C01,
	/// <summary>
	/// The APS layer attempted to send or deliver a message and failed
	/// </summary>
	SL_STATUS_ZIGBEE_DELIVERY_FAILED = 0x0C02,
	/// <summary>
	/// The maximum number of in-flight messages ::EMBER_APS_UNICAST_MESSAGE_COUNT has been reached
	/// </summary>
	SL_STATUS_ZIGBEE_MAX_MESSAGE_LIMIT_REACHED = 0x0C03,
	/// <summary>
	/// The application is trying to delete or overwrite a binding that is in use
	/// </summary>
	SL_STATUS_ZIGBEE_BINDING_IS_ACTIVE = 0x0C04,
	/// <summary>
	/// The application is trying to overwrite an address table entry that is in use
	/// </summary>
	SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE = 0x0C05,
	/// <summary>
	/// After moving, a mobile node&apos;s attempt to re-establish contact with the network failed
	/// </summary>
	SL_STATUS_ZIGBEE_MOVE_FAILED = 0x0C06,
	/// <summary>
	/// The local node ID has changed. The application can get the new node ID by calling ::sl_zigbee_get_node_id()
	/// </summary>
	SL_STATUS_ZIGBEE_NODE_ID_CHANGED = 0x0C07,
	/// <summary>
	/// The chosen security level is not supported by the stack
	/// </summary>
	SL_STATUS_ZIGBEE_INVALID_SECURITY_LEVEL = 0x0C08,
	/// <summary>
	/// An error occurred when trying to encrypt at the APS Level
	/// </summary>
	SL_STATUS_ZIGBEE_IEEE_ADDRESS_DISCOVERY_IN_PROGRESS = 0x0C09,
	/// <summary>
	/// An error occurred when trying to encrypt at the APS Level
	/// </summary>
	SL_STATUS_ZIGBEE_APS_ENCRYPTION_ERROR = 0x0C0A,
	/// <summary>
	/// There was an attempt to form or join a network with security without calling ::sl_zigbee_set_initial_security_state() first
	/// </summary>
	SL_STATUS_ZIGBEE_SECURITY_STATE_NOT_SET = 0x0C0B,
	/// <summary>
	/// There was an attempt to broadcast a key switch too quickly after broadcasting the next network key. The Trust Center must wait at least a period equal to the broadcast timeout so that all routers have a chance to receive the broadcast of the new network key
	/// </summary>
	SL_STATUS_ZIGBEE_TOO_SOON_FOR_SWITCH_KEY = 0x0C0C,
	/// <summary>
	/// The received signature corresponding to the message that was passed to the CBKE Library failed verification and is not valid
	/// </summary>
	SL_STATUS_ZIGBEE_SIGNATURE_VERIFY_FAILURE = 0x0C0D,
	/// <summary>
	/// The message could not be sent because the link key corresponding to the destination is not authorized for use in APS data messages
	/// </summary>
	SL_STATUS_ZIGBEE_KEY_NOT_AUTHORIZED = 0x0C0E,
	/// <summary>
	/// The application tried to use a binding that has been remotely modified and the change has not yet been reported to the application
	/// </summary>
	SL_STATUS_ZIGBEE_BINDING_HAS_CHANGED = 0x0C0F,
	/// <summary>
	/// The EUI of the Trust center has changed due to a successful rejoin after TC Swapout
	/// </summary>
	SL_STATUS_ZIGBEE_TRUST_CENTER_SWAP_EUI_HAS_CHANGED = 0x0C10,
	/// <summary>
	/// A Trust Center Swapout Rejoin has occurred without the EUI of the TC changing
	/// </summary>
	SL_STATUS_ZIGBEE_TRUST_CENTER_SWAP_EUI_HAS_NOT_CHANGED = 0x0C11,
	/// <summary>
	/// An attempt to generate random bytes failed because of insufficient random data from the radio
	/// </summary>
	SL_STATUS_ZIGBEE_INSUFFICIENT_RANDOM_DATA = 0x0C12,
	/// <summary>
	/// A Zigbee route error command frame was received indicating that a source routed message from this node failed en route
	/// </summary>
	SL_STATUS_ZIGBEE_SOURCE_ROUTE_FAILURE = 0x0C13,
	/// <summary>
	/// A Zigbee route error command frame was received indicating that a message sent to this node along a many-to-one route failed en route
	/// </summary>
	SL_STATUS_ZIGBEE_MANY_TO_ONE_ROUTE_FAILURE = 0x0C14,
	/// <summary>
	/// A critical and fatal error indicating that the version of the stack trying to run does not match with the chip it&apos;s running on
	/// </summary>
	SL_STATUS_ZIGBEE_STACK_AND_HARDWARE_MISMATCH = 0x0C15,
	/// <summary>
	/// The local PAN ID has changed. The application can get the new PAN ID by calling ::emberGetPanId()
	/// </summary>
	SL_STATUS_ZIGBEE_PAN_ID_CHANGED = 0x0C16,
	/// <summary>
	/// The channel has changed.
	/// </summary>
	SL_STATUS_ZIGBEE_CHANNEL_CHANGED = 0x0C17,
	/// <summary>
	/// The network has been opened for joining.
	/// </summary>
	SL_STATUS_ZIGBEE_NETWORK_OPENED = 0x0C18,
	/// <summary>
	/// The network has been closed for joining.
	/// </summary>
	SL_STATUS_ZIGBEE_NETWORK_CLOSED = 0x0C19,
	/// <summary>
	/// An attempt was made to join a Secured Network using a pre-configured key, but the Trust Center sent back a Network Key in-the-clear when an encrypted Network Key was required. (::EMBER_REQUIRE_ENCRYPTED_KEY)
	/// </summary>
	SL_STATUS_ZIGBEE_RECEIVED_KEY_IN_THE_CLEAR = 0x0C1A,
	/// <summary>
	/// An attempt was made to join a Secured Network, but the device did not receive a Network Key.
	/// </summary>
	SL_STATUS_ZIGBEE_NO_NETWORK_KEY_RECEIVED = 0x0C1B,
	/// <summary>
	/// After a device joined a Secured Network, a Link Key was requested (::EMBER_GET_LINK_KEY_WHEN_JOINING) but no response was ever received.
	/// </summary>
	SL_STATUS_ZIGBEE_NO_LINK_KEY_RECEIVED = 0x0C1C,
	/// <summary>
	/// An attempt was made to join a Secured Network without a pre-configured key, but the Trust Center sent encrypted data using a pre-configured key.
	/// </summary>
	SL_STATUS_ZIGBEE_PRECONFIGURED_KEY_REQUIRED = 0x0C1D,
	/// <summary>
	/// A Zigbee EZSP error has occured. Track the origin and corresponding EzspStatus for more info.
	/// </summary>
	SL_STATUS_ZIGBEE_EZSP_ERROR = 0x0C1E,
	/// <summary>
	/// Node ID discovery failed.
	/// </summary>
	SL_STATUS_ZIGBEE_ID_DISCOVERY_FAILED = 0x0C1F,
	/// <summary>
	/// Message was sent but no APS ACK received.
	/// </summary>
	SL_STATUS_ZIGBEE_NO_APS_ACK = 0x0C20,
	/// <summary>
	/// APS message was canceled.
	/// </summary>
	SL_STATUS_ZIGBEE_APS_MESSAGE_CANCELED = 0x0C21,
	/// <summary>
	/// Node ID discovery not enabled.
	/// </summary>
	SL_STATUS_ZIGBEE_ID_DISCOVERY_NOT_ENABLED = 0x0C22,
	/// <summary>
	/// Message was not sent, Node ID discovery is underway.
	/// </summary>
	SL_STATUS_ZIGBEE_ID_DISCOVERY_UNDERWAY = 0x0C23,
	/// <summary>
	/// The message was not sent because a route discovery is currently underway. There is no route to the target until the route discovery completes.
	/// </summary>
	SL_STATUS_ZIGBEE_SEND_UNICAST_ROUTE_DISCOVERY_UNDERWAY = 0x0C24,
	/// <summary>
	/// Radius is 0 or message has been dropped because route request failed or failed to submit message to tx queue.
	/// </summary>
	SL_STATUS_ZIGBEE_SEND_UNICAST_FAILURE = 0x0C25,
	/// <summary>
	/// No active route to the destination.
	/// </summary>
	SL_STATUS_ZIGBEE_SEND_UNICAST_NO_ROUTE = 0x0C26,
	/// <summary>
	/// Broadcast message timeout while waiting for sleepy children to poll.
	/// </summary>
	SL_STATUS_ZIGBEE_BROADCAST_TO_SLEEPY_CHILDREN_TIMEOUT = 0x0C27,
	/// <summary>
	/// Expected a neighbor to relay the message, but none did.
	/// </summary>
	SL_STATUS_ZIGBEE_BROADCAST_RELAY_FAILED = 0x0C28
}

#endif