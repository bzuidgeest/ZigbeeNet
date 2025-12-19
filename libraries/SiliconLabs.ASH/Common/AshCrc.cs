using System;

namespace SiliconLabs.ASH.Common
{
    /// <summary>
    /// Shared CRC utilities for ASH protocol
    /// </summary>
    public static class AshCrc
    {
        // CRC-CCITT lookup table (polynomial 0x1021)
        private static readonly ushort[] CrcTable = new ushort[256];
        
        static AshCrc()
        {
            // Initialize CRC lookup table
            for (int i = 0; i < 256; i++)
            {
                ushort crc = (ushort)(i << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc = (ushort)(crc << 1);
                }
                CrcTable[i] = crc;
            }
        }
        
        /// <summary>
        /// Compute standard 16-bit CRC-CCITT
        /// Used by ASH v2
        /// </summary>
        public static ushort ComputeCrc16(byte[] data, int offset = 0, int length = -1)
        {
            if (length < 0)
                length = data.Length - offset;
            
            ushort crc = 0xFFFF;
            
            for (int i = offset; i < offset + length; i++)
            {
                crc = (ushort)((crc << 8) ^ CrcTable[(crc >> 8) ^ data[i]]);
            }
            
            return crc;
        }
        
        /// <summary>
        /// Expand 16-bit CRC to 3 bytes (bit 4 cleared in all bytes)
        /// Used by ASH v3
        /// </summary>
        public static byte[] ExpandCrc(ushort crc)
        {
            byte[] expanded = new byte[3];
            
            // Extract bit 4 from each CRC byte
            byte bit4Byte1 = (byte)((crc >> 12) & 0x01);  // bit 12 of CRC
            byte bit4Byte2 = (byte)((crc >> 4) & 0x01);   // bit 4 of CRC
            
            // Clear bit 4 in both CRC bytes
            byte crcByte1 = (byte)((crc >> 8) & 0xEF);    // MSB with bit 4 cleared
            byte crcByte2 = (byte)(crc & 0xEF);           // LSB with bit 4 cleared
            
            expanded[0] = crcByte1;
            expanded[1] = crcByte2;
            expanded[2] = (byte)((bit4Byte1 << 7) | (bit4Byte2 << 6));
            
            return expanded;
        }
        
        /// <summary>
        /// Compress 3-byte expanded CRC back to 16-bit
        /// Used by ASH v3
        /// </summary>
        public static ushort CompressCrc(byte[] expanded)
        {
            if (expanded.Length < 3)
                throw new ArgumentException("Expanded CRC must be 3 bytes");
            
            byte crcByte1 = expanded[0];
            byte crcByte2 = expanded[1];
            byte bit4Flags = expanded[2];
            
            // Restore bit 4 in each byte
            if ((bit4Flags & 0x80) != 0)
                crcByte1 |= 0x10;
            
            if ((bit4Flags & 0x40) != 0)
                crcByte2 |= 0x10;
            
            return (ushort)((crcByte1 << 8) | crcByte2);
        }
    }
}
