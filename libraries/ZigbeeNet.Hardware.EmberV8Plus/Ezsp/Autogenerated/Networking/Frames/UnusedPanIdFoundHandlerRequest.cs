using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This function returns an unused panID and channel pair found via the find unused panId scan procedure.
/// Frame value: 0x00D2
/// </summary>
public class UnusedPanIdFoundHandlerRequest : EzspFrameRequest
{
