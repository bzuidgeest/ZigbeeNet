using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns a value indicating whether the node is joining, joined to, or leaving a network.
/// Frame value: 0x0018
/// </summary>
public class NetworkStateRequest : EzspFrameRequest
{
