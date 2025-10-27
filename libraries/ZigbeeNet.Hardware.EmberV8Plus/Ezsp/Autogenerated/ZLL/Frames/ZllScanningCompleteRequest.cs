using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Informs the ZLL API that application scanning is complete
/// Frame value: 0x00F6
/// </summary>
public class ZllScanningCompleteRequest : EzspFrameRequest
{
}
