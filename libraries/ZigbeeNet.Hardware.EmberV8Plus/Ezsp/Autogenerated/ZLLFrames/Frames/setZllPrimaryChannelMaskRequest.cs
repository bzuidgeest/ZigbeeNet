using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// Set the primary ZLL (touchlink) channel mask
/// Frame value: 0x00DB
/// </summary>
public class setZllPrimaryChannelMask : EzspFrameRequest
{
    /// <summary>
    /// The primary ZLL channel mask
    /// </summary>
    public uint zllPrimaryChannelMask { get; set; }

}
