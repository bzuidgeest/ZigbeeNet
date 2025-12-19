using SiliconLabs.ASH;
using System;

namespace SiliconLabs.ASH.Common
{
    /// <summary>
    /// Common interface for ASH frames (v2 and v3)
    /// </summary>
    public interface IAshFrame
    {
        /// <summary>
        /// Protocol version
        /// </summary>
        AshVersion Version { get; }
        
        /// <summary>
        /// Frame type
        /// </summary>
        AshFrameType FrameType { get; set; }
        
        /// <summary>
        /// Outgoing Frame Counter (sequence number) - 1 to 7
        /// v2: frmNum, v3: OFC
        /// </summary>
        byte OutgoingFrameCounter { get; set; }
        
        /// <summary>
        /// ACK/NAK Frame Counter (acknowledgement) - 0 to 7
        /// v2: ackNum, v3: AFC
        /// </summary>
        byte AckNackFrameCounter { get; set; }
        
        /// <summary>
        /// Retransmit flag
        /// </summary>
        bool IsRetransmit { get; set; }
        
        /// <summary>
        /// Data payload
        /// </summary>
        byte[] Data { get; set; }
        
        /// <summary>
        /// Whether this frame has a payload
        /// </summary>
        bool HasPayload { get; }
        
        /// <summary>
        /// Reset or error code (for RESET_ACK and ERROR frames)
        /// </summary>
        byte ResetCode { get; set; }
    }
}
