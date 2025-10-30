using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Get the number of configured endpoints.
/// Frame value: 0x012F
/// </summary>
public class GetEndpointCountRequest : EzspFrameRequest
{
