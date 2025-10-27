using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;

/// <summary>
/// Reset the node by calling halReboot.
/// Frame value: 0x0104
/// </summary>
public class ResetNodeResponse : EzspFrameResponse
{
}
