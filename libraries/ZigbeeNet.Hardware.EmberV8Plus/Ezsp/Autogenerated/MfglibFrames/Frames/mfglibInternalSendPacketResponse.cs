using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Structure;

/// <summary>
/// Sends a single packet consisting of the following bytes: packetLength, packetContents[0], ... , packetContents[packetLength - 3], CRC[0], CRC[1]. The total number of bytes sent is packetLength + 1. The radio replaces the last two bytes of packetContents[] with the 16-bit CRC for the packet.
/// Frame value: 0x0089
/// </summary>
public class mfglibInternalSendPacketResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
