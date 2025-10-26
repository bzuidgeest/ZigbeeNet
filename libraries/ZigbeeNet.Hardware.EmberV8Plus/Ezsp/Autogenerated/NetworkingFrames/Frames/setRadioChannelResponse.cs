using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets the channel to use for sending and receiving messages. For a list of available radio channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same channel.
/// Frame value: 0x009A
/// </summary>
public class setRadioChannelResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
