using System;
using ZigBeeNet.Util;
using Microsoft.Extensions.Logging;
using ZigbeeNet.Hardware.EmberV8Plus.Ezsp.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Internal.Serializer;

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

        /**
         * The minimum supported version of EZSP
         */
        private const int EZSP_MIN_VERSION = 8;

        /**
         * The maximum supported version of EZSP
         */
        private const int EZSP_MAX_VERSION = 14;

        /**
         * The current version of EZSP being used
         */
        protected static int ezspVersion = EZSP_MIN_VERSION;

        /**
         * EZSP Frame Control Request flag
         */
        protected const int EZSP_FC_REQUEST = 0x00;

        /**
         * EZSP Frame Control Response flag
         */
        protected const int EZSP_FC_RESPONSE = 0x80;

        protected int _sequenceNumber;
        protected int _frameControl;
        protected int _frameId = 0;
        protected bool _isResponse = false;

        /**
         * Sets the 8 bit transaction sequence number
         *
         * @param sequenceNumber
         */
        public void SetSequenceNumber(int sequenceNumber)
        {
            this._sequenceNumber = sequenceNumber;
        }

        /**
         * Gets the 8 bit transaction sequence number
         *
         * @return sequence number
         */
        public int GetSequenceNumber()
        {
            return _sequenceNumber;
        }

        /**
         * Checks if this frame is a response frame
         *
         * @return true if this is a response
         */
        public bool IsResponse()
        {
            return _isResponse;
        }

        /**
         * Gets the Ember frame ID for this frame
         *
         * @return the Ember frame Id
         */
        public int GetFrameId()
        {
            return _frameId;
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
        public static EzspFrameResponseV8Plus CreateHandler(int[] data)
        {
            Type ezspClass = null;
            EzspFrameResponseV8Plus ezspFrame = null;

            try
            {
                ezspClass = _ezspHandlerDict[data[3] + (data[4] << 8)];
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, "Error detecting the EZSP frame type");
            }

            if (ezspClass == null)
            {
                return null;
            }

            try
            {
                ezspFrame = (EzspFrameResponseV8Plus)Activator.CreateInstance(ezspClass, new object[] { data });
            }
            catch (Exception e)
            {
                _logger.LogDebug(e, "Error creating instance of EzspFrame");
            }

            return ezspFrame;
        }

        /**
         * Set the EZSP version to use
         *
         * @param ezspVersion the EZSP protocol version
         * @return true if the version is supported
         */
        public static bool SetEzspVersion(int ezspVersion)
        {
            if (ezspVersion <= EZSP_MAX_VERSION && ezspVersion >= EZSP_MIN_VERSION)
            {
                EzspFrameV8Plus.ezspVersion = ezspVersion;
                return true;
            }

            return false;
        }

        /**
         * Gets the current version of EZSP that is in use. This will default to the minimum supported version on startup
         *
         * @return the current version of EZSP
         */
        public static int GetEzspVersion()
        {
            return EzspFrameV8Plus.ezspVersion;
        }
    }
}
