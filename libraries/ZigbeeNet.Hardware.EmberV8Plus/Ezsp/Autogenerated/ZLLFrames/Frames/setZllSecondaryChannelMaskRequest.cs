using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// Set the secondary ZLL (touchlink) channel mask.
/// Frame value: 0x00DC
/// </summary>
public class setZllSecondaryChannelMask : EzspFrameRequest
{
    /// <summary>
    /// The secondary ZLL channel mask
    /// </summary>
    public uint zllSecondaryChannelMask { get; set; }

}
