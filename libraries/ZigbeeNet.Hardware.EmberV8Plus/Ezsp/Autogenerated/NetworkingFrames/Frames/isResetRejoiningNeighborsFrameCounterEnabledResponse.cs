using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Check whether a rejoining neighbor will have its incoming frame counter reset based on the currently set policy.
/// Frame value: 0x0125
/// </summary>
public class isResetRejoiningNeighborsFrameCounterEnabledResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether or not a rejoining neighbor&apos;s incoming FC gets reset (true or false).
    /// </summary>
    public bool getsReset { get; set; }

}
