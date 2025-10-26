using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the channel to use for sending and receiving messages. For a list of available radio channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same channel.
/// Frame value: 0x009A
/// </summary>
public class setRadioChannel : EzspFrameRequest
{
    /// <summary>
    /// Desired radio channel.
    /// </summary>
    public byte channel { get; set; }

}
