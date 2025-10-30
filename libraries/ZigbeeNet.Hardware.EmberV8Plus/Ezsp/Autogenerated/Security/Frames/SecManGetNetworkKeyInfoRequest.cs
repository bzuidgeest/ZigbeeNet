using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Retrieve information about the current and alternate network key, excluding their contents.
/// Frame value: 0x0116
/// </summary>
public class SecManGetNetworkKeyInfoRequest : EzspFrameRequest
{
