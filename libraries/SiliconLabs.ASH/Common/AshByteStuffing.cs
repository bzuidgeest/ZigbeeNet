using System;

namespace SiliconLabs.ASH.Common
{
    /// <summary>
    /// Shared byte stuffing utilities for ASH protocol
    /// Both v2 and v3 use the same byte stuffing mechanism
    /// </summary>
    public static class AshByteStuffing
    {
        // Reserved bytes (same for v2 and v3)
        public const byte FLAG = 0x7E;
        public const byte ESCAPE = 0x7D;
        public const byte XON = 0x11;
        public const byte XOFF = 0x13;
        public const byte SUBSTITUTE = 0x18;
        public const byte CANCEL = 0x1A;
        public const byte WAKEUP = 0xFF;
        
        public const byte ESCAPE_XOR = 0x20;
        
        /// <summary>
        /// Check if a byte needs to be escaped
        /// </summary>
        public static bool NeedsEscaping(byte b)
        {
            return b == FLAG || b == ESCAPE || b == XON || b == XOFF || 
                   b == SUBSTITUTE || b == CANCEL;
        }
        
        /// <summary>
        /// Byte stuff a frame (escape reserved bytes)
        /// </summary>
        public static byte[] StuffBytes(byte[] data)
        {
            int count = 0;
            
            // Count how many bytes need escaping
            foreach (byte b in data)
            {
                if (NeedsEscaping(b))
                    count++;
            }
            
            if (count == 0)
                return data;  // No stuffing needed
            
            // Create stuffed array
            byte[] stuffed = new byte[data.Length + count];
            int index = 0;
            
            foreach (byte b in data)
            {
                if (NeedsEscaping(b))
                {
                    stuffed[index++] = ESCAPE;
                    stuffed[index++] = (byte)(b ^ ESCAPE_XOR);
                }
                else
                {
                    stuffed[index++] = b;
                }
            }
            
            return stuffed;
        }
        
        /// <summary>
        /// Unstuff bytes (reverse byte stuffing)
        /// </summary>
        public static byte[] UnstuffBytes(byte[] data)
        {
            // Quick check if any escape bytes present
            bool hasEscape = false;
            foreach (byte b in data)
            {
                if (b == ESCAPE)
                {
                    hasEscape = true;
                    break;
                }
            }
            
            if (!hasEscape)
                return data;  // No unstuffing needed
            
            // Count actual bytes after unstuffing
            int count = 0;
            bool escaped = false;
            foreach (byte b in data)
            {
                if (escaped)
                {
                    count++;
                    escaped = false;
                }
                else if (b == ESCAPE)
                {
                    escaped = true;
                }
                else
                {
                    count++;
                }
            }
            
            // Unstuff
            byte[] unstuffed = new byte[count];
            int index = 0;
            escaped = false;
            
            foreach (byte b in data)
            {
                if (escaped)
                {
                    unstuffed[index++] = (byte)(b ^ ESCAPE_XOR);
                    escaped = false;
                }
                else if (b == ESCAPE)
                {
                    escaped = true;
                }
                else
                {
                    unstuffed[index++] = b;
                }
            }
            
            return unstuffed;
        }
    }
}
