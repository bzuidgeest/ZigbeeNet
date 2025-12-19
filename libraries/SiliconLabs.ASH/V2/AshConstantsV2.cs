namespace SiliconLabs.ASH.V2
{
    /// <summary>
    /// ASH v2 constants (UG101)
    /// </summary>
    public static class AshConstantsV2
    {
        // Frame sequence numbers (1-7)
        public const byte MinFrameCounter = 1;
        public const byte MaxFrameCounter = 7;
        
        // Window size (max unacknowledged frames)
        public const int WindowSize = 5;  // TX_K parameter per spec
        
        // Timeouts
        public const int RetransmissionTimeoutMs = 500;
        public const int MaxRetries = 3;  // ACK_TIMEOUTS parameter per spec
        
        // Data constraints
        public const int MaxDataLength = 128;  // Maximum EZSP frame size
        
        // Control byte values
        public const byte RstControlByte = 0xC0;      // RST: 11000000
        public const byte RStackControlByte = 0xC1;   // RSTACK: 11000001
        public const byte ErrorControlByte = 0xC2;    // ERROR: 11000010
        
        // Reset and error codes (Table 6.1 in UG101)
        public const byte ResetCode_Unknown = 0x00;
        public const byte ResetCode_External = 0x01;
        public const byte ResetCode_PowerOn = 0x02;
        public const byte ResetCode_Watchdog = 0x03;
        public const byte ResetCode_Assert = 0x06;
        public const byte ResetCode_Bootloader = 0x09;
        public const byte ResetCode_Software = 0x0B;
        public const byte ErrorCode_MaxAckTimeout = 0x51;
        public const byte ResetCode_ChipSpecific = 0x80;
        
        // Flow control parameters
        public const int T_RemoteNotReady_Ms = 1000;  // Time NCP waits after receiving nRdy before resuming
    }
}
