using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Get the current network.
/// Frame value: 0x014E
/// </summary>
public class GetCurrentNetworkRequest : EzspFrameRequest
{
