using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Get the secondary ZLL (touchlink) channel mask.
/// Frame value: 0x00DA
/// </summary>
public class getZllSecondaryChannelMaskResponse : EzspFrameResponse
{
    /// <summary>
    /// The secondary ZLL channel mask
    /// </summary>
    public uint zllSecondaryChannelMask { get; set; }

}
