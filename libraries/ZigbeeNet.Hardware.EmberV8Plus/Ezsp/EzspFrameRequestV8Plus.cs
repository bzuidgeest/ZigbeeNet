using System;
using System.Buffers.Binary;
using System.Threading;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp
{
    /// <summary>
    /// The EmberZNet Serial Protocol (EZSP) is the protocol used by a host application processor to interact with the
    /// EmberZNet PRO stack running on a Network CoProcessor(NCP).
    ///
    /// Reference: UG100: EZSP Reference Guide
    ///
    /// An EZSP V4 Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// 
    /// An EZSP V5+ Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Legacy Frame ID : 1 byte
    ///  - Extended Frame Control : 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// The Frame Control low byte is as follows -:
    /// 
    ///   bit 7 : 0 for Command
    ///   bit 6 : networkIndex[1]
    ///   bit 5 : networkIndex[0]
    ///   bit 4 : 0 (Reserved)
    ///   bit 3 : 0 (Reserved)
    ///   bit 2 : 0 (Reserved)
    ///   bit 1 : sleepMode[1]
    ///   bit 0 : sleepMode[0]
    /// </summary>
    public abstract class EzspFrameRequestV8Plus : EzspFrameV8Plus
    {
        private static int sequence = 0;
        
        public int NetworkIndex { get; set; } = 0;
        public SleepMode SleepMode { get; set; } = SleepMode.Idle;

        public bool SecurityEnabled { get; set; } = false;
        public bool PaddingEnabled { get; set; } = false;

        private const ushort frameVersion = 1;

        /**
         * Constructor used to create an outgoing frame
         */
        protected EzspFrameRequestV8Plus()
        {
            SequenceNumber = Interlocked.Increment(ref sequence) & 0xff;
        }

        protected byte[] CreateHeader(ushort frameId) 
        {
            Span<byte> buffer = stackalloc byte[5];
            // Output sequence number
            buffer[0] = (byte)SequenceNumber;
            // Output Frame Control Bytes
            buffer[1] = (byte)(EZSP_FC_REQUEST | ((NetworkIndex & 0x3) << 5) | (int)SleepMode);
            buffer[2] = (byte)(((SecurityEnabled ? 1 : 0) << 7) | (PaddingEnabled ? (1 << 6) : 0) | frameVersion);
            // Output Frame ID
            BinaryPrimitives.WriteUInt16LittleEndian(buffer.Slice(4), frameId);

            return buffer.ToArray();
        }

        protected abstract byte[] CreateParameters();
       
        public byte[] GetFrameBytes()
        {
            ushort frameId = _frameId;
            byte[] header = CreateHeader(frameId);
            byte[] parameters = CreateParameters();
            byte[] frame = new byte[header.Length + parameters.Length];
            Buffer.BlockCopy(header, 0, frame, 0, header.Length);
            Buffer.BlockCopy(parameters, 0, frame, header.Length, parameters.Length);
            return frame;
        }





    }
}
