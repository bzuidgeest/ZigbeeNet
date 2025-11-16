using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ZigbeeNet.Hardware.EmberV8Plus.Ezsp
{
    /// <summary>
    /// Defines MAC transmit complete parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ZigbeeMacTransmitComplete
    {
        /// <summary>
        /// MAC interface ID.
        /// </summary>
        public byte macInterfaceId;

        /// <summary>
        /// To identify a last outgoing flat packet.
        /// </summary>
        public byte tag;

        /// <summary>
        /// Transmitted packet status.
        /// </summary>
        public uint status;
    }

    /// <summary>
    /// Defines MAC transmit parameters.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ZigbeeMacTransmit
    {
        /// <summary>
        /// Length of outgoing flat packet.
        /// </summary>
        public byte length;

        /// <summary>
        /// Outgoing flat packet.
        /// </summary>
        public IntPtr outgoingFlatPacket;  // const uint8_t* - pointer to byte array

        /// <summary>
        /// Transmit complete info.
        /// </summary>
        public ZigbeeMacTransmitComplete txCompleteInfo;
    }

    public delegate void MacTransmitCallback(ZigbeeMacTransmit transmitParams);
}
