using System;
using System.Threading;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Internal.Serializer;

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
    public abstract class EzspFrameRequest : EzspFrameV8Plus
    {
        private static int sequence = 0;
        
        public int NetworkIndex { get; set; } = 0;
        public SleepMode SleepMode { get; set; } = SleepMode.Idle;

        /**
         * Constructor used to create an outgoing frame
         */
        protected EzspFrameRequest()
        {
            _sequenceNumber = Interlocked.Increment(ref sequence) & 0xff;
        }

        protected void SerializeHeader(EzspSerializer serializer) 
        {
            // Output sequence number
            serializer.SerializeUInt8(_sequenceNumber);

            // Output Frame Control Bytes
            // fix replace 0 with high byte properly.
            serializer.SerializeUInt16(((0) << 8) | (EZSP_FC_REQUEST | ((NetworkIndex & 0x3) << 5) | (int)SleepMode));

            // Output Frame ID
            serializer.SerializeUInt16(_frameId);
        }

        public virtual int[] Serialize() 
        {
            EzspSerializer serializer = new EzspSerializer();
            SerializeHeader(serializer);

            return serializer.GetPayload();
        }

         


    }
}
