using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Check whether a rejoining neighbor will have its incoming frame counter reset based on the currently set policy.
/// Frame value: 0x0125
/// </summary>
public class isResetRejoiningNeighborsFrameCounterEnabled : EzspFrameRequest
{
}
