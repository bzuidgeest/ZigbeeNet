using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Gets the current security state that is being used by a device that is joined in the network.
/// Frame value: 0x0069
/// </summary>
public class GetCurrentSecurityStateRequest : EzspFrameRequest
{
