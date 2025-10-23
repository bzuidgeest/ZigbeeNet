// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspStatus enumeration
    /// </summary>
    public enum EzspStatus
    {
        EZSP_SUCCESS = 0x00,
        /// <summary>
        /// Success.
        /// </summary>
        EZSP_SPI_ERR_FATAL = 0x10,
        /// <summary>
        /// Fatal error.
        /// </summary>
        EZSP_SPI_ERR_NCP_RESET = 0x11,
        /// <summary>
        /// The Response frame of the current transaction indicates the NCP has reset.
        /// </summary>
        EZSP_SPI_ERR_OVERSIZED_EZSP_FRAME = 0x12,
        /// <summary>
        /// oversized (the length byte is too large).
        /// </summary>
        EZSP_SPI_ERR_ABORTED_TRANSACTION = 0x13,
        /// <summary>
        /// transaction was aborted (nSSEL deasserted too soon).
        /// </summary>
        EZSP_SPI_ERR_MISSING_FRAME_TERMINATOR = 0x14,
        /// <summary>
        /// terminator is missing from the Command frame.
        /// </summary>
        EZSP_SPI_ERR_WAIT_SECTION_TIMEOUT = 0x15,
        /// <summary>
        /// WAIT_SECTION_TIMEOUT.
        /// </summary>
        EZSP_SPI_ERR_NO_FRAME_TERMINATOR = 0x16,
        /// <summary>
        /// The Response frame from the NCP is missing the frame terminator.
        /// </summary>
        EZSP_SPI_ERR_EZSP_COMMAND_OVERSIZED = 0x17,
        /// <summary>
        /// large) and the AVR's spi-protocol.c blocked the transmission.
        /// </summary>
        EZSP_SPI_ERR_EZSP_RESPONSE_OVERSIZED = 0x18,
        /// <summary>
        /// large) and the AVR's spi-protocol.c blocked the reception.
        /// </summary>
        EZSP_SPI_WAITING_FOR_RESPONSE = 0x19,
        /// <summary>
        /// Response.
        /// </summary>
        EZSP_SPI_ERR_HANDSHAKE_TIMEOUT = 0x1A,
        /// <summary>
        /// WAKE_HANDSHAKE_TIMEOUT.
        /// </summary>
        EZSP_SPI_ERR_STARTUP_TIMEOUT = 0x1B,
        /// <summary>
        /// defined by STARTUP_TIMEOUT.
        /// </summary>
        EZSP_SPI_ERR_STARTUP_FAIL = 0x1C,
        /// <summary>
        /// and the verification failed.
        /// </summary>
        EZSP_SPI_ERR_UNSUPPORTED_SPI_COMMAND = 0x1D,
        /// <summary>
        /// current mode the NCP is operating in.
        /// </summary>
        EZSP_ASH_IN_PROGRESS = 0x20,
        /// <summary>
        /// Operation not yet complete.
        /// </summary>
        EZSP_HOST_FATAL_ERROR = 0x21,
        /// <summary>
        /// Fatal error detected by host.
        /// </summary>
        EZSP_ASH_NCP_FATAL_ERROR = 0x22,
        /// <summary>
        /// Fatal error detected by NCP.
        /// </summary>
        EZSP_DATA_FRAME_TOO_LONG = 0x23,
        /// <summary>
        /// Tried to send DATA frame too long.
        /// </summary>
        EZSP_DATA_FRAME_TOO_SHORT = 0x24,
        /// <summary>
        /// Tried to send DATA frame too short.
        /// </summary>
        EZSP_NO_TX_SPACE = 0x25,
        /// <summary>
        /// No space for tx'ed DATA frame.
        /// </summary>
        EZSP_NO_RX_SPACE = 0x26,
        /// <summary>
        /// No space for rec'd DATA frame.
        /// </summary>
        EZSP_NO_RX_DATA = 0x27,
        /// <summary>
        /// No receive data available.
        /// </summary>
        EZSP_NOT_CONNECTED = 0x28,
        /// <summary>
        /// Not in Connected state.
        /// </summary>
        EZSP_ERROR_VERSION_NOT_SET = 0x30,
        /// <summary>
        /// The NCP received a command before the EZSP version had been set.
        /// </summary>
        EZSP_ERROR_INVALID_FRAME_ID = 0x31,
        /// <summary>
        /// The NCP received a command containing an unsupported frame ID.
        /// </summary>
        EZSP_ERROR_WRONG_DIRECTION = 0x32,
        /// <summary>
        /// The direction flag in the frame control field was incorrect.
        /// </summary>
        EZSP_ERROR_TRUNCATED = 0x33,
        /// <summary>
        /// would have exceeded the maximum EZSP frame length.
        /// </summary>
        EZSP_ERROR_OVERFLOW = 0x34,
        /// <summary>
        /// enough memory available to report them to the Host.
        /// </summary>
        EZSP_ERROR_OUT_OF_MEMORY = 0x35,
        /// <summary>
        /// Insufficient memory was available.
        /// </summary>
        EZSP_ERROR_INVALID_VALUE = 0x36,
        /// <summary>
        /// The value was out of bounds.
        /// </summary>
        EZSP_ERROR_INVALID_ID = 0x37,
        /// <summary>
        /// The configuration id was not recognized.
        /// </summary>
        EZSP_ERROR_INVALID_CALL = 0x38,
        /// <summary>
        /// Configuration values can no longer be modified.
        /// </summary>
        EZSP_ERROR_NO_RESPONSE = 0x39,
        /// <summary>
        /// The NCP failed to respond to a command.
        /// </summary>
        EZSP_ERROR_COMMAND_TOO_LONG = 0x40,
        /// <summary>
        /// The length of the command exceeded the maximum EZSP frame length.
        /// </summary>
        EZSP_ERROR_QUEUE_FULL = 0x41,
        /// <summary>
        /// The UART receive queue was full causing a callback response to be dropped.
        /// </summary>
        EZSP_ERROR_COMMAND_FILTERED = 0x42,
        /// <summary>
        /// The command has been filtered out by NCP.
        /// </summary>
        EZSP_ERROR_SECURITY_KEY_ALREADY_SET = 0x43,
        /// <summary>
        /// EZSP Security Key is already set
        /// </summary>
        EZSP_ERROR_SECURITY_TYPE_INVALID = 0x44,
        /// <summary>
        /// EZSP Security Type is invalid
        /// </summary>
        EZSP_ERROR_SECURITY_PARAMETERS_INVALID = 0x45,
        /// <summary>
        /// EZSP Security Parameters are invalid
        /// </summary>
        EZSP_ERROR_SECURITY_PARAMETERS_ALREADY_SET = 0x46,
        /// <summary>
        /// EZSP Security Parameters are already set
        /// </summary>
        EZSP_ERROR_SECURITY_KEY_NOT_SET = 0x47,
        /// <summary>
        /// EZSP Security Key is not set
        /// </summary>
        EZSP_ERROR_SECURITY_PARAMETERS_NOT_SET = 0x48,
        /// <summary>
        /// EZSP Security Parameters are not set
        /// </summary>
        EZSP_ERROR_UNSUPPORTED_CONTROL = 0x49,
        /// <summary>
        /// Received frame with unsupported control byte
        /// </summary>
        EZSP_ERROR_UNSECURE_FRAME = 0x4A,
        /// <summary>
        /// Received frame is unsecure, when security is established
        /// </summary>
        EZSP_ASH_ERROR_VERSION = 0x50,
        /// <summary>
        /// Incompatible ASH version
        /// </summary>
        EZSP_ASH_ERROR_TIMEOUTS = 0x51,
        /// <summary>
        /// Exceeded max ACK timeouts
        /// </summary>
        EZSP_ASH_ERROR_RESET_FAIL = 0x52,
        /// <summary>
        /// Timed out waiting for RSTACK
        /// </summary>
        EZSP_ASH_ERROR_NCP_RESET = 0x53,
        /// <summary>
        /// Unexpected ncp reset
        /// </summary>
        EZSP_ERROR_SERIAL_INIT = 0x54,
        /// <summary>
        /// Serial port initialization failed
        /// </summary>
        EZSP_ASH_ERROR_NCP_TYPE = 0x55,
        /// <summary>
        /// Invalid ncp processor type
        /// </summary>
        EZSP_ASH_ERROR_RESET_METHOD = 0x56,
        /// <summary>
        /// Invalid ncp reset method
        /// </summary>
        EZSP_ASH_ERROR_XON_XOFF = 0x57,
        /// <summary>
        /// XON/XOFF not supported by host driver
        /// </summary>
        EZSP_ASH_STARTED = 0x70,
        /// <summary>
        /// ASH protocol started
        /// </summary>
        EZSP_ASH_CONNECTED = 0x71,
        /// <summary>
        /// ASH protocol connected
        /// </summary>
        EZSP_ASH_DISCONNECTED = 0x72,
        /// <summary>
        /// ASH protocol disconnected
        /// </summary>
        EZSP_ASH_ACK_TIMEOUT = 0x73,
        /// <summary>
        /// Timer expired waiting for ack
        /// </summary>
        EZSP_ASH_CANCELLED = 0x74,
        /// <summary>
        /// Frame in progress cancelled
        /// </summary>
        EZSP_ASH_OUT_OF_SEQUENCE = 0x75,
        /// <summary>
        /// Received frame out of sequence
        /// </summary>
        EZSP_ASH_BAD_CRC = 0x76,
        /// <summary>
        /// Received frame with CRC error
        /// </summary>
        EZSP_ASH_COMM_ERROR = 0x77,
        /// <summary>
        /// Received frame with comm error
        /// </summary>
        EZSP_ASH_BAD_ACKNUM = 0x78,
        /// <summary>
        /// Received frame with bad ackNum
        /// </summary>
        EZSP_ASH_TOO_SHORT = 0x79,
        /// <summary>
        /// Received frame shorter than minimum
        /// </summary>
        EZSP_ASH_TOO_LONG = 0x7A,
        /// <summary>
        /// Received frame longer than maximum
        /// </summary>
        EZSP_ASH_BAD_CONTROL = 0x7B,
        /// <summary>
        /// Received frame with illegal control byte
        /// </summary>
        EZSP_ASH_BAD_LENGTH = 0x7C,
        /// <summary>
        /// Received frame with illegal length for its type
        /// </summary>
        EZSP_ASH_ACK_RECEIVED = 0x7D,
        /// <summary>
        /// Received ASH Ack
        /// </summary>
        EZSP_ASH_ACK_SENT = 0x7E,
        /// <summary>
        /// Sent ASH Ack
        /// </summary>
        EZSP_ASH_NAK_RECEIVED = 0x7F,
        /// <summary>
        /// Received ASH Nak
        /// </summary>
        EZSP_ASH_NAK_SENT = 0x80,
        /// <summary>
        /// Sent ASH Nak
        /// </summary>
        EZSP_ASH_RST_RECEIVED = 0x81,
        /// <summary>
        /// Received ASH RST
        /// </summary>
        EZSP_ASH_RST_SENT = 0x82,
        /// <summary>
        /// Sent ASH RST
        /// </summary>
        EZSP_ASH_STATUS = 0x83,
        /// <summary>
        /// ASH Status
        /// </summary>
        EZSP_ASH_TX = 0x84,
        /// <summary>
        /// ASH TX
        /// </summary>
        EZSP_ASH_RX = 0x85,
        /// <summary>
        /// ASH RX
        /// </summary>
        EZSP_CPC_ERROR_INIT = 0x86,
        /// <summary>
        /// Failed to connect to CPC daemon or failed to open CPC endpoint
        /// </summary>
        EZSP_NO_ERROR = 0xFF
    }
}
