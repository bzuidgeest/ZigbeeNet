using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Gets the channel in use for sending and receiving messages.
/// Frame value: 0x00FF
/// </summary>
public class getRadioChannelResponse : EzspFrameResponse
{
    /// <summary>
    /// Current radio channel.
    /// </summary>
    public byte channel { get; set; }

}
