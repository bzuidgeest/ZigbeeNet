using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Get the secondary ZLL (touchlink) channel mask.
/// Frame value: 0x00DA
/// </summary>
public class GetZllSecondaryChannelMaskResponse : EzspFrameResponse
{
    /// <summary>
    /// The secondary ZLL channel mask
    /// </summary>
    public uint zllSecondaryChannelMask { get; set; }

}
