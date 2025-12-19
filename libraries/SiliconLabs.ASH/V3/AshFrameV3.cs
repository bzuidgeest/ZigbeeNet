using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;

namespace SiliconLabs.ASH.V3
{
    /// <summary>
    /// ASH v3 frame implementation (UG115)
    /// v3: Data carried in ACK/NAK/RESET_ACK frames (no separate DATA frame type)
    /// </summary>
    public class AshFrameV3 : IAshFrame
    {
        public AshVersion Version => AshVersion.V3;
        
        public AshFrameType FrameType { get; set; }
        
        public byte OutgoingFrameCounter { get; set; }
        
        public byte AckNackFrameCounter { get; set; }
        
        public bool IsRetransmit { get; set; }
        
        public byte[] Data { get; set; } = Array.Empty<byte>();
        
        public bool HasPayload => Data.Length > 0;
        
        public byte ResetCode { get; set; }
        
        /// <summary>
        /// Creates an ACK frame (can carry data in v3)
        /// </summary>
        public static AshFrameV3 CreateAckFrame(byte ofc, byte afc, byte[]? data = null)
        {
            return new AshFrameV3
            {
                FrameType = AshFrameType.Ack,
                OutgoingFrameCounter = ofc,
                AckNackFrameCounter = afc,
                Data = data ?? Array.Empty<byte>()
            };
        }
        
        /// <summary>
        /// Creates a NACK frame (can carry data in v3)
        /// </summary>
        public static AshFrameV3 CreateNakFrame(byte ofc, byte afc, byte[]? data = null)
        {
            return new AshFrameV3
            {
                FrameType = AshFrameType.Nak,
                OutgoingFrameCounter = ofc,
                AckNackFrameCounter = afc,
                Data = data ?? Array.Empty<byte>()
            };
        }
        
        /// <summary>
        /// Creates a RESET frame (OFC=1, AFC=0, no payload)
        /// </summary>
        public static AshFrameV3 CreateResetFrame()
        {
            return new AshFrameV3
            {
                FrameType = AshFrameType.Reset,
                OutgoingFrameCounter = 1,
                AckNackFrameCounter = 0,
                Data = Array.Empty<byte>()
            };
        }
        
        /// <summary>
        /// Creates a RESET_ACK frame
        /// Empty payload: OFC=1, AFC=1
        /// With payload: OFC=2, AFC=1
        /// </summary>
        public static AshFrameV3 CreateResetAckFrame(byte[]? data = null)
        {
            bool hasData = data != null && data.Length > 0;
            return new AshFrameV3
            {
                FrameType = AshFrameType.ResetAck,
                OutgoingFrameCounter = (byte)(hasData ? 2 : 1),
                AckNackFrameCounter = 1,
                Data = data ?? Array.Empty<byte>()
            };
        }
        
        public override string ToString()
        {
            return $"v3: {FrameType} [OFC={OutgoingFrameCounter}, AFC={AckNackFrameCounter}, Payload={Data.Length} bytes]";
        }
    }
}
