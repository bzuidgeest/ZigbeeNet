using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Gets the channel in use for sending and receiving messages.
/// Frame value: 0x00FF
/// </summary>
public class GetRadioChannelResponse : EzspFrameResponse
{
    /// <summary>
    /// Current radio channel.
    /// </summary>
    public byte channel { get; set; }

}
