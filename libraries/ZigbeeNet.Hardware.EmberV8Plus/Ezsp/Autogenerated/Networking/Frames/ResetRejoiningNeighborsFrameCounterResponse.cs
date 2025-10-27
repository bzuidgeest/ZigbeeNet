using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate whether a rejoining neighbor should have its incoming frame counter reset.
/// Frame value: 0x0124
/// </summary>
public class ResetRejoiningNeighborsFrameCounterResponse : EzspFrameResponse
{
}
