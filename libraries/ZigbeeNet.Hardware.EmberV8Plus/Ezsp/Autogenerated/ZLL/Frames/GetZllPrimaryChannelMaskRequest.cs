using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Get the primary ZLL (touchlink) channel mask.
/// Frame value: 0x00D9
/// </summary>
public class GetZllPrimaryChannelMaskRequest : EzspFrameRequest
{
