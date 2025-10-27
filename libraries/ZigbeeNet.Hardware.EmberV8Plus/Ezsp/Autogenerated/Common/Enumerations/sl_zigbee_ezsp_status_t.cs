namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Status values used by EZSP.
/// </summary>
public enum ZigbeeEzspStatus : byte
{
    /// <summary>
    /// Success.
    /// </summary>
    SL_ZIGBEE_EZSP_SUCCESS = 0x00,
    /// <summary>
    /// Fatal error.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_FATAL = 0x10,
    /// <summary>
    /// The Response frame of the current transaction indicates the NCP has reset.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_NCP_RESET = 0x11,
    /// <summary>
    /// The NCP is reporting that the Command frame of the current transaction is oversized (the length byte is too large).
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_OVERSIZED_SL_ZIGBEE_EZSP_FRAME = 0x12,
    /// <summary>
    /// The Response frame of the current transaction indicates the previous transaction was aborted (nSSEL deasserted too soon).
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_ABORTED_TRANSACTION = 0x13,
    /// <summary>
    /// The Response frame of the current transaction indicates the frame terminator is missing from the Command frame.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_MISSING_FRAME_TERMINATOR = 0x14,
    /// <summary>
    /// The NCP has not provided a Response within the time limit defined by WAIT_SECTION_TIMEOUT.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_WAIT_SECTION_TIMEOUT = 0x15,
    /// <summary>
    /// The Response frame from the NCP is missing the frame terminator.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_NO_FRAME_TERMINATOR = 0x16,
    /// <summary>
    /// The Host attempted to send an oversized Command (the length byte is too large) and the AVR&apos;s spi-protocol.c blocked the transmission.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_SL_ZIGBEE_EZSP_COMMAND_OVERSIZED = 0x17,
    /// <summary>
    /// The NCP attempted to send an oversized Response (the length byte is too large) and the AVR&apos;s spi-protocol.c blocked the reception.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_SL_ZIGBEE_EZSP_RESPONSE_OVERSIZED = 0x18,
    /// <summary>
    /// The Host has sent the Command and is still waiting for the NCP to send a Response.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_WAITING_FOR_RESPONSE = 0x19,
    /// <summary>
    /// The NCP has not asserted nHOST_INT within the time limit defined by WAKE_HANDSHAKE_TIMEOUT.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_HANDSHAKE_TIMEOUT = 0x1A,
    /// <summary>
    /// The NCP has not asserted nHOST_INT after an NCP reset within the time limit defined by STARTUP_TIMEOUT.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_STARTUP_TIMEOUT = 0x1B,
    /// <summary>
    /// The Host attempted to verify the SPI Protocol activity and version number, and the verification failed.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_STARTUP_FAIL = 0x1C,
    /// <summary>
    /// The Host has sent a command with a SPI Byte that is unsupported by the current mode the NCP is operating in.
    /// </summary>
    SL_ZIGBEE_EZSP_SPI_ERR_UNSUPPORTED_SPI_COMMAND = 0x1D,
    /// <summary>
    /// Operation not yet complete.
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_IN_PROGRESS = 0x20,
    /// <summary>
    /// Fatal error detected by host.
    /// </summary>
    SL_ZIGBEE_EZSP_HOST_FATAL_ERROR = 0x21,
    /// <summary>
    /// Fatal error detected by NCP.
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_NCP_FATAL_ERROR = 0x22,
    /// <summary>
    /// Tried to send DATA frame too long.
    /// </summary>
    SL_ZIGBEE_EZSP_DATA_FRAME_TOO_LONG = 0x23,
    /// <summary>
    /// Tried to send DATA frame too short.
    /// </summary>
    SL_ZIGBEE_EZSP_DATA_FRAME_TOO_SHORT = 0x24,
    /// <summary>
    /// No space for tx&apos;ed DATA frame.
    /// </summary>
    SL_ZIGBEE_EZSP_NO_TX_SPACE = 0x25,
    /// <summary>
    /// No space for rec&apos;d DATA frame.
    /// </summary>
    SL_ZIGBEE_EZSP_NO_RX_SPACE = 0x26,
    /// <summary>
    /// No receive data available.
    /// </summary>
    SL_ZIGBEE_EZSP_NO_RX_DATA = 0x27,
    /// <summary>
    /// Not in Connected state.
    /// </summary>
    SL_ZIGBEE_EZSP_NOT_CONNECTED = 0x28,
    /// <summary>
    /// The NCP received a command before the EZSP version had been set.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_VERSION_NOT_SET = 0x30,
    /// <summary>
    /// The NCP received a command containing an unsupported frame ID.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_INVALID_FRAME_ID = 0x31,
    /// <summary>
    /// The direction flag in the frame control field was incorrect.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_WRONG_DIRECTION = 0x32,
    /// <summary>
    /// The truncated flag in the frame control field was set, indicating there was not enough memory available to complete the response or that the response would have exceeded the maximum EZSP frame length.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_TRUNCATED = 0x33,
    /// <summary>
    /// The overflow flag in the frame control field was set, indicating one or more callbacks occurred since the previous response and there was not enough memory available to report them to the Host.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_OVERFLOW = 0x34,
    /// <summary>
    /// Insufficient memory was available.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_OUT_OF_MEMORY = 0x35,
    /// <summary>
    /// The value was out of bounds.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE = 0x36,
    /// <summary>
    /// The configuration id was not recognized.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_INVALID_ID = 0x37,
    /// <summary>
    /// Configuration values can no longer be modified.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_INVALID_CALL = 0x38,
    /// <summary>
    /// The NCP failed to respond to a command.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_NO_RESPONSE = 0x39,
    /// <summary>
    /// The length of the command exceeded the maximum EZSP frame length.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_COMMAND_TOO_LONG = 0x40,
    /// <summary>
    /// The UART receive queue was full causing a callback response to be dropped.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_QUEUE_FULL = 0x41,
    /// <summary>
    /// The command has been filtered out by NCP.
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_COMMAND_FILTERED = 0x42,
    /// <summary>
    /// EZSP Security Key is already set
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_KEY_ALREADY_SET = 0x43,
    /// <summary>
    /// EZSP Security Type is invalid
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_TYPE_INVALID = 0x44,
    /// <summary>
    /// EZSP Security Parameters are invalid
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_PARAMETERS_INVALID = 0x45,
    /// <summary>
    /// EZSP Security Parameters are already set
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_PARAMETERS_ALREADY_SET = 0x46,
    /// <summary>
    /// EZSP Security Key is not set
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_KEY_NOT_SET = 0x47,
    /// <summary>
    /// EZSP Security Parameters are not set
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SECURITY_PARAMETERS_NOT_SET = 0x48,
    /// <summary>
    /// Received frame with unsupported control byte
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_UNSUPPORTED_CONTROL = 0x49,
    /// <summary>
    /// Received frame is unsecure, when security is established
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_UNSECURE_FRAME = 0x4A,
    /// <summary>
    /// Incompatible ASH version
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_VERSION = 0x50,
    /// <summary>
    /// Exceeded max ACK timeouts
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_TIMEOUTS = 0x51,
    /// <summary>
    /// Timed out waiting for RSTACK
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_RESET_FAIL = 0x52,
    /// <summary>
    /// Unexpected ncp reset
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_NCP_RESET = 0x53,
    /// <summary>
    /// Serial port initialization failed
    /// </summary>
    SL_ZIGBEE_EZSP_ERROR_SERIAL_INIT = 0x54,
    /// <summary>
    /// Invalid ncp processor type
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_NCP_TYPE = 0x55,
    /// <summary>
    /// Invalid ncp reset method
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_RESET_METHOD = 0x56,
    /// <summary>
    /// XON/XOFF not supported by host driver
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ERROR_XON_XOFF = 0x57,
    /// <summary>
    /// ASH protocol started
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_STARTED = 0x70,
    /// <summary>
    /// ASH protocol connected
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_CONNECTED = 0x71,
    /// <summary>
    /// ASH protocol disconnected
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_DISCONNECTED = 0x72,
    /// <summary>
    /// Timer expired waiting for ack
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ACK_TIMEOUT = 0x73,
    /// <summary>
    /// Frame in progress cancelled
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_CANCELLED = 0x74,
    /// <summary>
    /// Received frame out of sequence
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_OUT_OF_SEQUENCE = 0x75,
    /// <summary>
    /// Received frame with CRC error
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_BAD_CRC = 0x76,
    /// <summary>
    /// Received frame with comm error
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_COMM_ERROR = 0x77,
    /// <summary>
    /// Received frame with bad ackNum
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_BAD_ACKNUM = 0x78,
    /// <summary>
    /// Received frame shorter than minimum
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_TOO_SHORT = 0x79,
    /// <summary>
    /// Received frame longer than maximum
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_TOO_LONG = 0x7A,
    /// <summary>
    /// Received frame with illegal control byte
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_BAD_CONTROL = 0x7B,
    /// <summary>
    /// Received frame with illegal length for its type
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_BAD_LENGTH = 0x7C,
    /// <summary>
    /// Received ASH Ack
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ACK_RECEIVED = 0x7D,
    /// <summary>
    /// Sent ASH Ack
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_ACK_SENT = 0x7E,
    /// <summary>
    /// Received ASH Nak
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_NAK_RECEIVED = 0x7F,
    /// <summary>
    /// Sent ASH Nak
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_NAK_SENT = 0x80,
    /// <summary>
    /// Received ASH RST
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_RST_RECEIVED = 0x81,
    /// <summary>
    /// Sent ASH RST
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_RST_SENT = 0x82,
    /// <summary>
    /// ASH Status
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_STATUS = 0x83,
    /// <summary>
    /// ASH TX
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_TX = 0x84,
    /// <summary>
    /// ASH RX
    /// </summary>
    SL_ZIGBEE_EZSP_ASH_RX = 0x85,
    /// <summary>
    /// Failed to connect to CPC daemon or failed to open CPC endpoint
    /// </summary>
    SL_ZIGBEE_EZSP_CPC_ERROR_INIT = 0x86,
    /// <summary>
    /// No reset or error
    /// </summary>
    SL_ZIGBEE_EZSP_NO_ERROR = 0xFF
}
