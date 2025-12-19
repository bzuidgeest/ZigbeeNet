using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;
using System.Collections.Generic;

namespace SiliconLabs.ASH.V2
{
    /// <summary>
    /// ASH v2 frame codec (UG101)
    /// Features: DATA frame type, 2-byte CRC, data randomization
    /// </summary>
    public class AshFrameCodecV2 : IAshFrameCodec
    {
        private readonly ILogger? _logger;
        
        public AshVersion Version => AshVersion.V2;
        
        public AshFrameCodecV2(ILogger? logger = null)
        {
            _logger = logger;
        }
        
        public byte[] EncodeFrame(IAshFrame frame)
        {
            var frameV2 = frame as AshFrameV2 ?? throw new ArgumentException("Frame must be AshFrameV2");

            List<byte> frameBytes = new List<byte>();

            // Build control byte
            byte control = BuildControlByte(frameV2);
            frameBytes.Add(control);

            // Add data field with randomization (for DATA frames)
            if (frameV2.HasPayload)
            {
                byte[] dataToSend = frameV2.Data;

                // Apply randomization for DATA frames
                if (frameV2.FrameType == AshFrameType.Data)
                {
                    dataToSend = ApplyRandomization(frameV2.Data);
                }

                frameBytes.AddRange(dataToSend);
            }

            // Compute and append 2-byte CRC (standard, not expanded)
            ushort crc = AshCrc.ComputeCrc16(frameBytes.ToArray());
            frameBytes.Add((byte)(crc >> 8));    // MSB first (big-endian)
            frameBytes.Add((byte)(crc & 0xFF));  // LSB

            // Byte stuff the frame (escape reserved bytes)
            byte[] stuffed = AshByteStuffing.StuffBytes(frameBytes.ToArray());

            // Build final frame with FLAG delimiter at the end
            List<byte> finalFrame = new List<byte>();
            finalFrame.AddRange(stuffed);
            finalFrame.Add(AshByteStuffing.FLAG);  // FLAG byte at end

            return finalFrame.ToArray();
        }
        
        public IAshFrame? DecodeFrame(byte[] frameBytes)
        {
            if (frameBytes.Length < 3)  // Min: CONTROL + CRC(2)
                return null;
            
            // Extract CRC (last 2 bytes)
            int crcIndex = frameBytes.Length - 2;
            ushort receivedCrc = (ushort)((frameBytes[crcIndex] << 8) | frameBytes[crcIndex + 1]);
            
            // Compute CRC on everything except CRC bytes
            ushort computedCrc = AshCrc.ComputeCrc16(frameBytes, 0, crcIndex);
            
            if (receivedCrc != computedCrc)
            {
                if (_logger != null && _logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("CRC mismatch: received={ReceivedCrc:X4}, computed={ComputedCrc:X4}", 
                        receivedCrc, computedCrc);
                }
                return null;
            }
            
            byte control = frameBytes[0];
            AshFrameV2 frame = new AshFrameV2();
            
            // Parse control byte based on v2 format
            if ((control & 0x80) == 0)
            {
                // DATA frame: bit7=0, frmNum in bits 6-4, reTx in bit 3, ackNum in bits 2-0
                frame.FrameType = AshFrameType.Data;
                frame.OutgoingFrameCounter = (byte)((control >> 4) & 0x07);
                frame.AckNackFrameCounter = (byte)(control & 0x07);
                frame.IsRetransmit = (control & 0x08) != 0;
                
                // Extract and de-randomize data
                if (frameBytes.Length > 3)
                {
                    byte[] data = new byte[frameBytes.Length - 3];
                    Array.Copy(frameBytes, 1, data, 0, data.Length);
                    frame.Data = ApplyRandomization(data);  // XOR is reversible
                }
            }
            else if ((control & 0xE0) == 0x80)
            {
                // ACK frame: 100xxxxx where bit 3=nRdy, bits 2-0=ackNum
                frame.FrameType = AshFrameType.Ack;
                frame.AckNackFrameCounter = (byte)(control & 0x07);
                frame.NotReady = (control & 0x08) != 0;
            }
            else if ((control & 0xE0) == 0xA0)
            {
                // NAK frame: 101xxxxx where bit 3=nRdy, bits 2-0=ackNum
                frame.FrameType = AshFrameType.Nak;
                frame.AckNackFrameCounter = (byte)(control & 0x07);
                frame.NotReady = (control & 0x08) != 0;
            }
            else if (control == 0xC0)
            {
                // RST frame: 11000000
                frame.FrameType = AshFrameType.Reset;
            }
            else if (control == 0xC1)
            {
                // RSTACK frame: 11000001
                frame.FrameType = AshFrameType.ResetAck;
                
                // Extract version and reset code
                if (frameBytes.Length >= 5)  // CONTROL + VERSION + RESET_CODE + CRC(2)
                {
                    frame.ResetCode = frameBytes[2];
                    frame.Data = new byte[] { frameBytes[1], frameBytes[2] };
                }
            }
            else if (control == 0xC2)
            {
                // ERROR frame: 11000010
                frame.FrameType = AshFrameType.Error;
                
                // Extract version and error code
                if (frameBytes.Length >= 5)
                {
                    frame.ResetCode = frameBytes[2];
                    frame.Data = new byte[] { frameBytes[1], frameBytes[2] };
                }
            }
            else
            {
                // Unknown control byte
                if (_logger != null)
                {
                    _logger.LogWarning("Unknown v2 control byte: {Control:X2}", control);
                }
                return null;
            }
            
            return frame;
        }
        
        public bool CanDecode(byte[] frameBytes)
        {
            if (frameBytes.Length < 1)
                return false;
            
            byte control = frameBytes[0];
            
            // v2 frames:
            // - DATA: bit7=0
            // - ACK: 100xxxxx
            // - NAK: 101xxxxx
            // - RST: 11000000
            // - RSTACK: 11000001
            // - ERROR: 11000010
            
            if ((control & 0x80) == 0)
                return true;  // DATA frame
            
            if ((control & 0xE0) == 0x80 || (control & 0xE0) == 0xA0)
                return true;  // ACK or NAK
            
            if (control == 0xC0 || control == 0xC1 || control == 0xC2)
                return true;  // RST, RSTACK, or ERROR
            
            return false;
        }
        
        private byte BuildControlByte(AshFrameV2 frame)
        {
            byte control = 0;
            
            switch (frame.FrameType)
            {
                case AshFrameType.Data:
                    // DATA: 0xxx xxxx where bits 6-4=frmNum, bit 3=reTx, bits 2-0=ackNum
                    control = (byte)((frame.OutgoingFrameCounter & 0x07) << 4);
                    control |= (byte)(frame.AckNackFrameCounter & 0x07);
                    if (frame.IsRetransmit)
                        control |= 0x08;
                    break;
                
                case AshFrameType.Ack:
                    // ACK: 100x xxxx where bit 3=nRdy, bits 2-0=ackNum
                    control = 0x80;
                    control |= (byte)(frame.AckNackFrameCounter & 0x07);
                    if (frame.NotReady)
                        control |= 0x08;
                    break;
                
                case AshFrameType.Nak:
                    // NAK: 101x xxxx where bit 3=nRdy, bits 2-0=ackNum
                    control = 0xA0;
                    control |= (byte)(frame.AckNackFrameCounter & 0x07);
                    if (frame.NotReady)
                        control |= 0x08;
                    break;
                
                case AshFrameType.Reset:
                    // RST: 11000000
                    control = 0xC0;
                    break;
                
                case AshFrameType.ResetAck:
                    // RSTACK: 11000001
                    control = 0xC1;
                    break;
                
                case AshFrameType.Error:
                    // ERROR: 11000010
                    control = 0xC2;
                    break;
                
                default:
                    throw new ArgumentException($"Invalid frame type for v2: {frame.FrameType}");
            }
            
            return control;
        }
        
        /// <summary>
        /// Apply/remove data randomization (XOR with pseudo-random sequence)
        /// Sequence: {0x42, 0x21, 0xA8, 0x54, 0x2A, ...}
        /// Algorithm: if bit 0 = 0: rand_next = rand >> 1
        ///           if bit 0 = 1: rand_next = (rand >> 1) ^ 0xB8
        /// </summary>
        private byte[] ApplyRandomization(byte[] data)
        {
            byte[] randomized = new byte[data.Length];
            byte rand = 0x42;  // Initial value per spec
            
            for (int i = 0; i < data.Length; i++)
            {
                randomized[i] = (byte)(data[i] ^ rand);
                
                // Update pseudo-random sequence
                if ((rand & 0x01) == 0)
                    rand = (byte)(rand >> 1);
                else
                    rand = (byte)((rand >> 1) ^ 0xB8);
            }
            
            return randomized;
        }
    }
}
