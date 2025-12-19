namespace SiliconLabs.ASH.V3
{
    /// <summary>
    /// ASH v3 constants (UG115)
    /// </summary>
    public static class AshConstantsV3
    {
        // Frame sequence numbers (1-7)
        public const byte MinFrameCounter = 1;
        public const byte MaxFrameCounter = 7;
        
        // Window size (max unacknowledged frames)
        public const int WindowSize = 2;  // Max 2 frames in flight per spec
        
        // Timeouts
        public const int RetransmissionTimeoutMs = 500;
        public const int MaxRetries = 3;
        
        // Data constraints
        public const int MaxDataLength = 57;  // Maximum payload size per spec
        
        // Frame length constraints
        public const int MinFrameLength = 4;   // Minimum: HEADER + CONTROL + LENGTH + CRC(3)
        public const int MaxFrameLength = 136; // Maximum frame size
        
        // Header bytes
        public const byte FlagByte = 0x7E;
        public const byte EscapeByte = 0x7D;
        public const byte XonByte = 0x11;
        public const byte XoffByte = 0x13;
        public const byte SubstituteByte = 0x18;
        public const byte CancelByte = 0x1A;
        public const byte WakeupByte = 0xFF;
        
        // Escape XOR value
        public const byte EscapeXor = 0x20;
        
        // Header escape bit flags
        public const byte HeaderEscapePayloadLengthBit = 0x04;
        public const byte HeaderEscapeRetransmitBit = 0x08;
    }
}
