using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Is there a ZLL (Touchlink) operation in progress?
/// Frame value: 0x00D7
/// </summary>
public class ZllOperationInProgressRequest : EzspFrameRequest
{
