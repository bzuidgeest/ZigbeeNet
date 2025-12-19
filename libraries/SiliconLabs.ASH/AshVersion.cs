namespace SiliconLabs.ASH
{
    /// <summary>
    /// ASH protocol version
    /// </summary>
    public enum AshVersion : byte
    {
        /// <summary>
        /// Version unknown or not yet detected
        /// </summary>
        Unknown = 0x00,
        
        /// <summary>
        /// ASH Protocol Version 2 (UG101)
        /// Features: DATA frame type, 2-byte CRC, data randomization
        /// </summary>
        V2 = 0x02,
        
        /// <summary>
        /// ASH Protocol Version 3 (UG115)
        /// Features: No DATA frame, 3-byte expanded CRC, 4-byte header
        /// </summary>
        V3 = 0x03
    }
    
    /// <summary>
    /// Extension methods for AshVersion
    /// </summary>
    public static class AshVersionExtensions
    {
        /// <summary>
        /// Check if version is known
        /// </summary>
        public static bool IsKnown(this AshVersion version)
        {
            return version == AshVersion.V2 || version == AshVersion.V3;
        }
        
        /// <summary>
        /// Get human-readable version string
        /// </summary>
        public static string ToDisplayString(this AshVersion version)
        {
            return version switch
            {
                AshVersion.V2 => "ASH v2 (UG101)",
                AshVersion.V3 => "ASH v3 (UG115)",
                _ => "Unknown"
            };
        }
        
        /// <summary>
        /// Check if version uses data randomization
        /// </summary>
        public static bool UsesDataRandomization(this AshVersion version)
        {
            return version == AshVersion.V2;
        }
        
        /// <summary>
        /// Check if version uses 3-byte expanded CRC
        /// </summary>
        public static bool UsesExpandedCrc(this AshVersion version)
        {
            return version == AshVersion.V3;
        }
        
        /// <summary>
        /// Check if version uses 4-byte header (HEADER_ESCAPE byte)
        /// </summary>
        public static bool UsesFourByteHeader(this AshVersion version)
        {
            return version == AshVersion.V3;
        }
        
        /// <summary>
        /// Check if version has dedicated DATA frame type
        /// </summary>
        public static bool HasDataFrameType(this AshVersion version)
        {
            return version == AshVersion.V2;
        }
    }
}
