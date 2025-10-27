using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Is the ZLL radio on when idle mode is active?
/// Frame value: 0x00D8
/// </summary>
public class ZllRxOnWhenIdleGetActiveRequest : EzspFrameRequest
{
}
