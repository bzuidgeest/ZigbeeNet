using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Sends a single packet consisting of the following bytes: packetLength, packetContents[0], ... , packetContents[packetLength - 3], CRC[0], CRC[1]. The total number of bytes sent is packetLength + 1. The radio replaces the last two bytes of packetContents[] with the 16-bit CRC for the packet.
/// Frame value: 0x0089
/// </summary>
public class MfglibInternalSendPacketRequest : EzspFrameRequest
{
    /// <summary>
    /// The length of the packetContents parameter in bytes. Must be greater than 3 and less than 123.
    /// </summary>
    public byte packetLength { get; set; }

    /// <summary>
    /// The packet to send. The last two bytes will be replaced with the 16-bit CRC.
    /// </summary>
    public uint8_t[packetLength] packetContents { get; set; }

