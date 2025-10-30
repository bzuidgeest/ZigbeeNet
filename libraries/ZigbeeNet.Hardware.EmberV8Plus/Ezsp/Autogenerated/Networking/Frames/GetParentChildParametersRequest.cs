using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns information about the children of the local node and the parent of the local node.
/// Frame value: 0x0029
/// </summary>
public class GetParentChildParametersRequest : EzspFrameRequest
{
