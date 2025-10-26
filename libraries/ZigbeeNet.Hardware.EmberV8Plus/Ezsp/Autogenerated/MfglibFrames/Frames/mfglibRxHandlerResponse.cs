using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Structure;

/// <summary>
/// A callback indicating a packet with a valid CRC has been received.
/// Frame value: 0x008e
/// </summary>
public class mfglibRxHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The link quality observed during the reception
    /// </summary>
    public byte linkQuality { get; set; }

    /// <summary>
    /// The energy level (in units of dBm) observed during the reception.
    /// </summary>
    public sbyte rssi { get; set; }

    /// <summary>
    /// The length of the packetContents parameter in bytes. Will be greater than 3 and less than 123.
    /// </summary>
    public byte packetLength { get; set; }

    /// <summary>
    /// The received packet (last 2 bytes are not FCS / CRC and may be discarded)
    /// </summary>
    public uint8_t[packetLength] packetContents { get; set; }

}
