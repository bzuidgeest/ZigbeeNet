using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;
using System.Collections.Generic;

namespace SiliconLabs.ASH.V3
{
    /// <summary>
    /// ASH v3 frame codec implementation (UG115)
    /// Features: 3-byte expanded CRC, 4-byte header, no data randomization
    /// </summary>
    public class AshFrameCodecV3 : IAshFrameCodec
    {
        private readonly ILogger? _logger;
        
        public AshVersion Version => AshVersion.V3;
        
        public AshFrameCodecV3(ILogger? logger = null)
        {
            _logger = logger;
        }
        
        public byte[] EncodeFrame(IAshFrame frame)
        {
            var frameV3 = frame as AshFrameV3 ?? ConvertToV3Frame(frame);
            
            // Build control byte
            byte control = BuildControlByte(frameV3.FrameType, frameV3.OutgoingFrameCounter, frameV3.AckNackFrameCounter);
            byte payloadLength = (byte)frameV3.Data.Length;
            
            // Determine if control or payload length need escaping
            bool controlNeedsEscape = AshByteStuffing.NeedsEscaping(control);
            bool payloadLengthNeedsEscape = AshByteStuffing.NeedsEscaping(payloadLength);
            
            // Build header escape byte
            byte headerEscape = 0;
            if (controlNeedsEscape)
                headerEscape |= V3Constants.HeaderEscapeControlBit;
            if (payloadLengthNeedsEscape)
                headerEscape |= V3Constants.HeaderEscapePayloadLengthBit;
            
            // Build frame for CRC calculation
            List<byte> frameForCrc = new List<byte>();
            frameForCrc.Add(control);
            frameForCrc.Add(payloadLength);
            frameForCrc.AddRange(frameV3.Data);
            
            // Calculate CRC and expand to 3 bytes
            ushort crc = AshCrc.ComputeCrc16(frameForCrc.ToArray());
            byte[] expandedCrc = AshCrc.ExpandCrc(crc);
            
            // Build complete frame before stuffing
            List<byte> completeFrame = new List<byte>();
            completeFrame.Add(control);
            completeFrame.Add(payloadLength);
            completeFrame.AddRange(frameV3.Data);
            completeFrame.AddRange(expandedCrc);
            
            // Byte stuff the frame
            byte[] stuffed = AshByteStuffing.StuffBytes(completeFrame.ToArray());
            
            // Build final frame with FLAG and header escape
            List<byte> finalFrame = new List<byte>();
            finalFrame.Add(AshByteStuffing.FLAG);
            finalFrame.Add(headerEscape);
            finalFrame.AddRange(stuffed);
            
            return finalFrame.ToArray();
        }
        
        public IAshFrame? DecodeFrame(byte[] frameData)
        {
            if (frameData == null || frameData.Length < 5) // control + payload_length + 3-byte CRC minimum
                return null;
            
            try
            {
                // First byte is header escape (passed separately in original design, but included here)
                byte headerEscape = 0;
                int dataOffset = 0;
                
                // Check if first byte looks like header escape (typically 0 or small value)
                if (frameData[0] < 0x10)
                {
                    headerEscape = frameData[0];
                    dataOffset = 1;
                }
                
                byte control = frameData[dataOffset];
                byte payloadLength = frameData[dataOffset + 1];
                
                // Apply header escape if needed
                if ((headerEscape & V3Constants.HeaderEscapeControlBit) != 0)
                    control ^= AshByteStuffing.ESCAPE_XOR;
                if ((headerEscape & V3Constants.HeaderEscapePayloadLengthBit) != 0)
                    payloadLength ^= AshByteStuffing.ESCAPE_XOR;
                
                // Extract frame type, OFC, AFC from control byte
                byte type = (byte)((control & V3Constants.TypeMask) >> V3Constants.TypeShift);
                byte ofc = (byte)((control & V3Constants.OfcMask) >> V3Constants.OfcShift);
                byte afc = (byte)(control & V3Constants.AfcMask);
                
                if (!Enum.IsDefined(typeof(AshFrameType), type))
                    return null;
                
                // Validate frame counter range (1-7)
                if (type != (byte)AshFrameType.Reset)
                {
                    if (ofc < AshConstantsV3.MinFrameCounter || ofc > AshConstantsV3.MaxFrameCounter)
                        return null;
                }
                
                // Extract payload
                int expectedLength = dataOffset + 2 + payloadLength + 3; // offset + control + length + data + 3-byte CRC
                if (frameData.Length != expectedLength)
                    return null;
                
                byte[] data = new byte[payloadLength];
                if (payloadLength > 0)
                    Array.Copy(frameData, dataOffset + 2, data, 0, payloadLength);
                
                // Extract and verify CRC
                byte[] expandedCrc = new byte[3];
                Array.Copy(frameData, frameData.Length - 3, expandedCrc, 0, 3);
                
                // Calculate expected CRC
                byte[] dataForCrc = new byte[2 + payloadLength];
                dataForCrc[0] = control;
                dataForCrc[1] = payloadLength;
                if (payloadLength > 0)
                    Array.Copy(data, 0, dataForCrc, 2, payloadLength);
                
                ushort calculatedCrc = AshCrc.ComputeCrc16(dataForCrc);
                ushort receivedCrc = AshCrc.CompressCrc(expandedCrc);
                
                if (calculatedCrc != receivedCrc)
                    return null;
                
                // Create frame
                return new AshFrameV3
                {
                    FrameType = (AshFrameType)type,
                    OutgoingFrameCounter = ofc,
                    AckNackFrameCounter = afc,
                    Data = data
                };
            }
            catch
            {
                return null;
            }
        }
        
        public bool CanDecode(byte[] frameBytes)
        {
            if (frameBytes.Length < 1)
                return false;
            
            byte control = frameBytes[0];
            
            // v3 uses frame type in upper 2 bits
            // Valid v3 frame types: 0 (RESET), 1 (RESET_ACK), 2 (ACK), 3 (NACK)
            byte frameType = (byte)((control >> 6) & 0x03);
            
            return frameType <= 3;
        }
        
        private byte BuildControlByte(AshFrameType type, byte ofc, byte afc)
        {
            byte control = (byte)((byte)type << V3Constants.TypeShift);
            control |= (byte)((ofc & V3Constants.FrameCounterMask) << V3Constants.OfcShift);
            control |= (byte)(afc & V3Constants.FrameCounterMask);
            return control;
        }
        
        private AshFrameV3 ConvertToV3Frame(IAshFrame frame)
        {
            return new AshFrameV3
            {
                FrameType = frame.FrameType,
                OutgoingFrameCounter = frame.OutgoingFrameCounter,
                AckNackFrameCounter = frame.AckNackFrameCounter,
                IsRetransmit = frame.IsRetransmit,
                Data = frame.Data,
                ResetCode = frame.ResetCode
            };
        }
        
        /// <summary>
        /// V3-specific constants for frame encoding
        /// </summary>
        private static class V3Constants
        {
            public const byte TypeShift = 6;
            public const byte OfcShift = 3;
            public const byte TypeMask = 0xC0;
            public const byte OfcMask = 0x38;
            public const byte AfcMask = 0x07;
            public const byte FrameCounterMask = 0x07;
            
            public const byte HeaderEscapeControlBit = 0x01;
            public const byte HeaderEscapePayloadLengthBit = 0x04;
        }
    }
}
