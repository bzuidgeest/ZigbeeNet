using SiliconLabs.ASH.V2;
using SiliconLabs.ASH.V3;
using Microsoft.Extensions.Logging;
using SiliconLabs.ASH.Common;
using System;

namespace SiliconLabs.ASH
{
    /// <summary>
    /// Factory for creating version-specific frame codecs
    /// </summary>
    public static class AshFrameCodecFactory
    {
        /// <summary>
        /// Create codec for specified version
        /// </summary>
        public static IAshFrameCodec Create(AshVersion version, ILogger? logger = null)
        {
            return version switch
            {
                AshVersion.V2 => new AshFrameCodecV2(logger),
                AshVersion.V3 => new AshFrameCodecV3(logger),
                _ => throw new ArgumentException($"Unsupported ASH version: {version}")
            };
        }
        
        /// <summary>
        /// Detect version from RSTACK/RESET_ACK frame bytes
        /// </summary>
        public static AshVersion DetectVersion(byte[] frameBytes)
        {
            if (frameBytes.Length < 3)
                return AshVersion.Unknown;
            
            byte control = frameBytes[0];
            
            // v2 RSTACK: control byte 0xC1, version byte at index 1
            if (control == 0xC1 && frameBytes.Length >= 3)
            {
                byte version = frameBytes[1];
                if (version == 0x02)
                    return AshVersion.V2;
            }
            
            // v3 RESET_ACK: control byte indicates frame type in upper bits
            // Check for v3 pattern (frame type 1 = RESET_ACK)
            if ((control & 0xC0) == 0x40)  // Frame type 1 (RESET_ACK)
            {
                return AshVersion.V3;
            }
            
            // Try v2 codec
            var v2Codec = new AshFrameCodecV2();
            if (v2Codec.CanDecode(frameBytes))
                return AshVersion.V2;
            
            // Try v3 codec
            var v3Codec = new AshFrameCodecV3();
            if (v3Codec.CanDecode(frameBytes))
                return AshVersion.V3;
            
            // Default to v3
            return AshVersion.V3;
        }
    }
}
