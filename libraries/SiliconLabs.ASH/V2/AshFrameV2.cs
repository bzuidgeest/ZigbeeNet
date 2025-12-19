using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;

namespace SiliconLabs.ASH.V2
{
    /// <summary>
    /// ASH v2 frame implementation (UG101)
    /// </summary>
    public class AshFrameV2 : IAshFrame
    {
        public AshVersion Version => AshVersion.V2;
        
        public AshFrameType FrameType { get; set; }
        
        public byte OutgoingFrameCounter { get; set; }
        
        public byte AckNackFrameCounter { get; set; }
        
        public bool IsRetransmit { get; set; }
        
        /// <summary>
        /// Not Ready flag (v2 specific) - inhibits NCP callback frames
        /// </summary>
        public bool NotReady { get; set; }
        
        public byte[] Data { get; set; } = Array.Empty<byte>();
        
        public bool HasPayload => Data.Length > 0;
        
        public byte ResetCode { get; set; }
        
        /// <summary>
        /// Create DATA frame
        /// </summary>
        public static AshFrameV2 CreateDataFrame(byte frmNum, byte ackNum, byte[] data)
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.Data,
                OutgoingFrameCounter = frmNum,
                AckNackFrameCounter = ackNum,
                Data = data
            };
        }
        
        /// <summary>
        /// Create ACK frame
        /// </summary>
        public static AshFrameV2 CreateAckFrame(byte ackNum, bool notReady = false)
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.Ack,
                AckNackFrameCounter = ackNum,
                NotReady = notReady
            };
        }
        
        /// <summary>
        /// Create NAK frame
        /// </summary>
        public static AshFrameV2 CreateNakFrame(byte ackNum, bool notReady = false)
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.Nak,
                AckNackFrameCounter = ackNum,
                NotReady = notReady
            };
        }
        
        /// <summary>
        /// Create RST frame
        /// </summary>
        public static AshFrameV2 CreateResetFrame()
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.Reset
            };
        }
        
        /// <summary>
        /// Create RSTACK frame
        /// </summary>
        public static AshFrameV2 CreateResetAckFrame(byte resetCode)
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.ResetAck,
                ResetCode = resetCode,
                Data = new byte[] { 0x02, resetCode }  // Version 0x02, reset code
            };
        }
        
        /// <summary>
        /// Create ERROR frame
        /// </summary>
        public static AshFrameV2 CreateErrorFrame(byte errorCode)
        {
            return new AshFrameV2
            {
                FrameType = AshFrameType.Error,
                ResetCode = errorCode,
                Data = new byte[] { 0x02, errorCode }  // Version 0x02, error code
            };
        }
        
        public override string ToString()
        {
            string frameTypeStr = FrameType switch
            {
                AshFrameType.Data => "DATA",
                AshFrameType.Ack => "ACK",
                AshFrameType.Nak => "NAK",
                AshFrameType.Reset => "RST",
                AshFrameType.ResetAck => "RSTACK",
                AshFrameType.Error => "ERROR",
                _ => FrameType.ToString()
            };
            
            return $"{frameTypeStr} [frmNum={OutgoingFrameCounter}, ackNum={AckNackFrameCounter}, " +
                   $"reTx={IsRetransmit}, nRdy={NotReady}, Payload={Data.Length} bytes]";
        }
    }
}
