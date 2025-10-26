using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the channel for desired phy interface to use for sending and receiving messages. For a list of available radio pages and channels, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API, as all devices on a network must use the same page and channel.
/// Frame value: 0x00FB
/// </summary>
public class multiPhySetRadioChannel : EzspFrameRequest
{
    /// <summary>
    /// Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.
    /// </summary>
    public byte phyIndex { get; set; }

    /// <summary>
    /// Desired radio channel page.
    /// </summary>
    public byte page { get; set; }

    /// <summary>
    /// Desired radio channel.
    /// </summary>
    public byte channel { get; set; }

}
