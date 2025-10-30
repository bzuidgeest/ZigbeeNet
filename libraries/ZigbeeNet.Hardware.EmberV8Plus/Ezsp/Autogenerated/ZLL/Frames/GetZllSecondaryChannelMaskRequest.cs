using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Get the secondary ZLL (touchlink) channel mask.
/// Frame value: 0x00DA
/// </summary>
public class GetZllSecondaryChannelMaskRequest : EzspFrameRequest
{
