using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when network and group addresses are assigned to a remote mode in a network start or network join request.
/// Frame value: 0x00B8
/// </summary>
public class ZllAddressAssignmentHandlerRequest : EzspFrameRequest
{
}
