namespace SiliconLabs.ASH
{
    /// <summary>
    /// ASH frame types
    /// 
    /// ASH v3 (current implementation):
    /// - RESET (0), RESET_ACK (1), ACK (2), NACK (3)
    /// - Data is carried in ACK, NACK, and RESET_ACK frames
    /// 
    /// ASH v2 (UG101):
    /// - RST, RSTACK, DATA, ACK, NAK, ERROR
    /// - Dedicated DATA frame type for carrying EZSP frames
    /// </summary>
    public enum AshFrameType : byte
    {
        /// <summary>
        /// Reset frame (v2: RST, v3: RESET)
        /// Requests NCP to perform software reset
        /// </summary>
        Reset = 0,
        
        /// <summary>
        /// Reset acknowledgement (v2: RSTACK, v3: RESET_ACK)
        /// Sent by NCP after reset with version and reset code
        /// </summary>
        ResetAck = 1,
        
        /// <summary>
        /// Acknowledgement frame (v2: ACK, v3: ACK)
        /// v2: Acknowledges DATA frames only
        /// v3: Can carry data payload (piggybacked data)
        /// </summary>
        Ack = 2,
        
        /// <summary>
        /// Negative acknowledgement (v2: NAK, v3: NACK)
        /// v2: Indicates DATA frame error
        /// v3: Can carry data payload
        /// </summary>
        Nak = 3,
        
        /// <summary>
        /// Data frame (v2 only)
        /// Carries EZSP frames
        /// In v2: Control byte bit 7 = 0 indicates DATA frame
        /// In v3: This type doesn't exist; data is carried in ACK/NAK/RESET_ACK
        /// </summary>
        Data = 4,  // Virtual type for v2 DATA frames
        
        /// <summary>
        /// Error frame (v2 only)
        /// Sent by NCP when entering FAILED state
        /// </summary>
        Error = 5  // v2 only
    }
}
