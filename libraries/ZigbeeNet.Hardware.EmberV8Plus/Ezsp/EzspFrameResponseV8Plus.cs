using System;
using System.Buffers.Binary;
using System.Linq;
using System.Threading;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp
{
    /// <summary>
    /// The EmberZNet Serial Protocol (EZSP) is the protocol used by a host application processor to interact with the
    /// EmberZNet PRO stack running on a Network CoProcessor(NCP).
    ///
    /// Reference: UG600: EZSP Reference Guide
    /// 
    /// An EZSP Frame is made up as follows -:
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
    ///   bit 7 : 1 for Response
    ///   bit 6 : networkIndex[1]
    ///   bit 5 : networkIndex[0]
    ///   bit 4 : callbackType[1]
    ///   bit 3 : callbackType[0]
    ///   bit 2 : callbackPending
    ///   bit 1 : truncated
    ///   bit 0 : overflow
    /// 
    /// </summary>
    public abstract partial class EzspFrameResponseV8Plus : EzspFrameV8Plus
    {
        //protected EzspDeserializer deserializer;

        private const int EZSP_FC_CB_PENDING = 0x04;

        private bool _callbackPending = false;

        /**
         * Constructor used to create a received frame. The constructor reads the header fields from the incoming message.
         *
         * @param inputBuffer the input array to deserialize
         */
        protected EzspFrameResponseV8Plus()
        {
            

            
        }

        public bool IsSupportedResponse()
        {
            //fix
            return _ezspHandlerDict.Keys.Contains(_frameId);
        }

        /*Use this method in derived classes to parse the header if needed
         * @param frameBytes the input array to deserialize
         * @return the index after parsing the header
         */
        internal static EmberResponseHeader ParseHeader(ReadOnlySpan<byte> frameBytes)
        {
            return new EmberResponseHeader(
				frameBytes[0], 
				BinaryPrimitives.ReadUInt16LittleEndian(frameBytes.Slice(1, 2)), 
				BinaryPrimitives.ReadUInt16LittleEndian(frameBytes.Slice(3, 2)));

			// Fix -> move to frame parser or somthing
			/*
            _isResponse = (_frameControl & EZSP_FC_RESPONSE) != 0;
            _callbackPending = (_frameControl & EZSP_FC_CB_PENDING) != 0;

            */
        }



        /**
         * Returns true if the frame control byte indicates that a callback is pending for this response frame
         *
         * @return true if a callback is pending
         */
        public bool IsCallbackPending()
        {
            return _callbackPending;
        }

        public CallbackType CallbackType()
        {
            return (CallbackType)((_frameControl & 0x18) >> 3);
        }

        public bool IsTruncated()
        {
            return (_frameControl & 0x2) == 0x2;
        }

        public bool HasOverflowed()
        {
            return (_frameControl & 0x1) == 0x1;
        }

        public int NetworkIndex()
        {
            return (_frameControl & 0x60) >> 5;
        }

    }

}
