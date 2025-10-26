using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets the channel for desired phy interface to use for sending and receiving messages. For a list of available radio pages and channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same page and channel.
/// Frame value: 0x00FB
/// </summary>
public class multiPhySetRadioChannelResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
