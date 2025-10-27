using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Returns number of phy interfaces present.
/// Frame value: 0x00FC
/// </summary>
public class GetPhyInterfaceCountRequest : EzspFrameRequest
{
}
