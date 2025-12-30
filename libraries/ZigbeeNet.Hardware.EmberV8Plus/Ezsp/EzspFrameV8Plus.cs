using Microsoft.Extensions.Logging;
using System;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations;
using ZigBeeNet.Util;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp
{
    /// <summary>
    /// The EmberZNet Serial Protocol (EZSP) is the protocol used by a host application processor to interact with the
    /// EmberZNet PRO stack running on a Network CoProcessor(NCP).
    /// 
    /// Version command uses the oldest frame format so every version of EZSP can understand it.
    ///
    /// Reference: UG600: EZSP Reference Guide
    ///
    /// An EZSP V4 Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// 
    /// An EZSP V5-V7 Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Legacy Frame ID : 1 byte
    ///  - Extended Frame Control : 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// An EZSP V8+ Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 2 byte
    ///  - Frame ID : 2 byte
    ///  - Parameters : variable length
    /// 
    /// The Frame Control high byte is as follows -:
    /// 
    ///   bit 7 : securityEnabled
    ///   bit 6 : paddingEnabled
    ///   bit 5 : 0 (Reserved)
    ///   bit 4 : 0 (Reserved)
    ///   bit 3 : 0 (Reserved)
    ///   bit 2 : 0 (Reserved)
    ///   bit 1 : frameFormatVersion[1]
    ///   bit 0 : frameFormatVersion[0]
    /// </summary>
    public abstract partial class EzspFrameV8Plus
    {
        static private readonly ILogger _logger = LogManager.GetLog<EzspFrameV8Plus>();

        //public abstract ushort FrameId { get; }

        /**
         * EZSP Frame Control Request flag
         */
        protected const int EZSP_FC_REQUEST = 0x00;

        /**
         * EZSP Frame Control Response flag
         */
        protected const int EZSP_FC_RESPONSE = 0x80;

        protected int _frameControl;
        protected bool _isResponse = false;
		

        /**
         * Sets the 8 bit transaction sequence number
         *
         * @param sequenceNumber
         */
        public int SequenceNumber { get; set; }

        /**
         * Checks if this frame is a response frame
         *
         * @return true if this is a response
         */
        public bool IsResponse()
        {
            return _isResponse;
        }

        

        public bool IsSecurityEnabled => (_frameControl & 0x8000) != 0;

        public bool IsPaddingEnabled => (_frameControl & 0x4000) != 0;

        public FrameFormatVersion FrameFormatVersion => (FrameFormatVersion)(_frameControl & 0x0003);

        /**
         * Creates and {@link EzspFrameResponse} from the incoming data.
         *
         * @param data the int[] containing the EZSP data from which to generate the frame
         * @return the {@link EzspFrameResponse} or null if the response can't be created.
         */
        //fix
        //public static EzspFrameResponseV8Plus CreateHandler(int[] data)
        //{
        //    Type ezspClass = null;
        //    EzspFrameResponseV8Plus ezspFrame = null;

        //    try
        //    {
        //        ezspClass = _ezspHandlerDict[data[3] + (data[4] << 8)];
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogDebug(e, "Error detecting the EZSP frame type");
        //    }

        //    if (ezspClass == null)
        //    {
        //        return null;
        //    }

        //    try
        //    {
        //        ezspFrame = (EzspFrameResponseV8Plus)Activator.CreateInstance(ezspClass, new object[] { data });
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogDebug(e, "Error creating instance of EzspFrame");
        //    }

        //    return ezspFrame;
        //}



	}
}
