using SiliconLabs.ASH;
using System;

namespace SiliconLabs.ASH.Common
{
    /// <summary>
    /// Common interface for ASH frame codecs (v2 and v3)
    /// </summary>
    public interface IAshFrameCodec
    {
        /// <summary>
        /// Protocol version this codec handles
        /// </summary>
        AshVersion Version { get; }
        
        /// <summary>
        /// Encode an ASH frame into wire format
        /// </summary>
        /// <param name="frame">Frame to encode</param>
        /// <returns>Encoded frame bytes (excluding FLAG, including any byte stuffing)</returns>
        byte[] EncodeFrame(IAshFrame frame);
        
        /// <summary>
        /// Decode wire format bytes into an ASH frame
        /// </summary>
        /// <param name="frameBytes">Frame bytes (after unstuffing, excluding FLAG)</param>
        /// <returns>Decoded frame, or null if invalid</returns>
        IAshFrame? DecodeFrame(byte[] frameBytes);
        
        /// <summary>
        /// Check if this codec can handle the given frame bytes
        /// </summary>
        bool CanDecode(byte[] frameBytes);
    }
}
