// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Protocol
{
    /// <summary>
    /// EZSP Protocol constants and definitions
    /// </summary>
    public static class EzspProtocol
    {
        /// <summary>
        /// Protocol Definitions
        /// </summary>
        public const byte EZSP_PROTOCOL_VERSION = 0x0D;
        /// <summary>
        /// EZSP Sequence Index for both legacy and extended frame format
        /// </summary>
        public const byte EZSP_SEQUENCE_INDEX = 0;
        /// <summary>
        /// Legacy EZSP Frame Format
        /// </summary>
        public const byte EZSP_MIN_FRAME_LENGTH = 3;
        /// <summary>
        /// Legacy EZSP Frame Format
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_INDEX = 1;
        /// <summary>
        /// Legacy EZSP Frame Format
        /// </summary>
        public const byte EZSP_FRAME_ID_INDEX = 2;
        /// <summary>
        /// Legacy EZSP Frame Format
        /// </summary>
        public const byte EZSP_PARAMETERS_INDEX = 3;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_MIN_FRAME_LENGTH = 5;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_ID_LENGTH = 2;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_LB_INDEX = 1;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_HB_INDEX = 2;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_ID_LB_INDEX = 3;
        /// <summary>
        /// Extended EZSP Frame Format
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_ID_HB_INDEX = 4;
        public const byte EZSP_EXTENDED_PARAMETERS_INDEX = 5;
        public const byte EZSP_STACK_TYPE_MESH = 0x02;
        /// <summary>
        /// from the EM260 to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_DIRECTION_MASK = 0x80;
        /// <summary>
        /// from the EM260 to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_COMMAND = 0x00;
        /// <summary>
        /// from the EM260 to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_RESPONSE = 0x80;
        /// <summary>
        /// frame control.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NETWORK_INDEX_MASK = 0x60;
        /// <summary>
        /// frame control.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NETWORK_INDEX_OFFSET = 5;
        /// <summary>
        /// it has sent its response.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_SLEEP_MODE_MASK = 0x03;
        /// <summary>
        /// it has sent its response.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_IDLE = 0x00;
        /// <summary>
        /// Processor idle.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_DEEP_SLEEP = 0x01;
        /// <summary>
        /// Wake on interrupt or timer.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_POWER_DOWN = 0x02;
        /// <summary>
        /// Wake on interrupt only.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_RESERVED_SLEEP = 0x03;
        /// <summary>
        /// enough memory available to report them to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_OVERFLOW_MASK = 0x01;
        /// <summary>
        /// enough memory available to report them to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NO_OVERFLOW = 0x00;
        /// <summary>
        /// enough memory available to report them to the Host.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_OVERFLOW = 0x01;
        /// <summary>
        /// exceeded the maximum EZSP frame length.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_TRUNCATED_MASK = 0x02;
        /// <summary>
        /// exceeded the maximum EZSP frame length.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NOT_TRUNCATED = 0x00;
        /// <summary>
        /// exceeded the maximum EZSP frame length.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_TRUNCATED = 0x02;
        /// <summary>
        /// clear if the response to a callback command read the last pending callback.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_PENDING_CB_MASK = 0x04;
        /// <summary>
        /// clear if the response to a callback command read the last pending callback.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_PENDING_CB = 0x04;
        /// <summary>
        /// clear if the response to a callback command read the last pending callback.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NO_PENDING_CB = 0x00;
        /// <summary>
        /// this ezsp frame is the response to an ezspCallback().
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_SYNCH_CB_MASK = 0x08;
        /// <summary>
        /// this ezsp frame is the response to an ezspCallback().
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_SYNCH_CB = 0x08;
        /// <summary>
        /// this ezsp frame is the response to an ezspCallback().
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NOT_SYNCH_CB = 0x00;
        /// <summary>
        /// be set only in the uart version when EZSP_VALUE_UART_SYNCH_CALLBACKS is 0.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_ASYNCH_CB_MASK = 0x10;
        /// <summary>
        /// be set only in the uart version when EZSP_VALUE_UART_SYNCH_CALLBACKS is 0.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_ASYNCH_CB = 0x10;
        /// <summary>
        /// be set only in the uart version when EZSP_VALUE_UART_SYNCH_CALLBACKS is 0.
        /// </summary>
        public const byte EZSP_FRAME_CONTROL_NOT_ASYNCH_CB = 0x00;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_SECURITY_MASK = 0x80;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_SECURE = 0x80;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_UNSECURE = 0x00;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_PADDING_MASK = 0x40;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_PADDED = 0x40;
        /// <summary>
        /// enabled or not.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_UNPADDED = 0x00;
        /// <summary>
        /// frame format version.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_FORMAT_VERSION_MASK = 0x03;
        /// <summary>
        /// frame format version.
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_FORMAT_VERSION = 0x01;
        /// <summary>
        /// Reserved bits 2-5
        /// </summary>
        public const byte EZSP_EXTENDED_FRAME_CONTROL_RESERVED_MASK = 0x3C;
    }
}
