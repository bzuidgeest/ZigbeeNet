using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Get the primary ZLL (touchlink) channel mask.
/// Frame value: 0x00D9
/// </summary>
public class getZllPrimaryChannelMaskResponse : EzspFrameResponse
{
    /// <summary>
    /// The primary ZLL channel mask
    /// </summary>
    public uint zllPrimaryChannelMask { get; set; }

}
