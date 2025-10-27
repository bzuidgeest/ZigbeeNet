using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Returns the 16-bit node ID of the local node.
/// Frame value: 0x0027
/// </summary>
public class GetNodeIdRequest : EzspFrameRequest
{
}
