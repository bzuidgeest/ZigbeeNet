using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Indicate whether a rejoining neighbor should have its incoming frame counter reset.
/// Frame value: 0x0124
/// </summary>
public class resetRejoiningNeighborsFrameCounterResponse : EzspFrameResponse
{
}
