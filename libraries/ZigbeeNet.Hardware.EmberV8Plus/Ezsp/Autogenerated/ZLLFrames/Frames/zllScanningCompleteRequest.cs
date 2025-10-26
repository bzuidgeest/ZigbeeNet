using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// Informs the ZLL API that application scanning is complete
/// Frame value: 0x00F6
/// </summary>
public class zllScanningComplete : EzspFrameRequest
{
}
