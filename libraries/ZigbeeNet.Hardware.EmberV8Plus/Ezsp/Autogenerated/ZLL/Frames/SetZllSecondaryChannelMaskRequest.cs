using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Set the secondary ZLL (touchlink) channel mask.
/// Frame value: 0x00DC
/// </summary>
public class SetZllSecondaryChannelMaskRequest : EzspFrameRequest
{
    /// <summary>
    /// The secondary ZLL channel mask
    /// </summary>
    public uint zllSecondaryChannelMask { get; set; }

}
