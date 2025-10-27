using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Set the primary ZLL (touchlink) channel mask
/// Frame value: 0x00DB
/// </summary>
public class SetZllPrimaryChannelMaskRequest : EzspFrameRequest
{
    /// <summary>
    /// The primary ZLL channel mask
    /// </summary>
    public uint zllPrimaryChannelMask { get; set; }

}
