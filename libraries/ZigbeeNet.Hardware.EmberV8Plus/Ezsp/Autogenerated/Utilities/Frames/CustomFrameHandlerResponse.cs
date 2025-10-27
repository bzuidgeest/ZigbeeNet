using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// A callback indicating a custom EZSP message has been received.
/// Frame value: 0x0054
/// </summary>
public class CustomFrameHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The length of the custom frame payload.
    /// </summary>
    public byte payloadLength { get; set; }

    /// <summary>
    /// The payload of the custom frame.
    /// </summary>
    public uint8_t[payloadLength] payload { get; set; }

}
