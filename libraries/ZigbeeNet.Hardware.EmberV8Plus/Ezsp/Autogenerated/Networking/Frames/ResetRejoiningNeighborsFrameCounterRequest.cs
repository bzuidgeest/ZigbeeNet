using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate whether a rejoining neighbor should have its incoming frame counter reset.
/// Frame value: 0x0124
/// </summary>
public class ResetRejoiningNeighborsFrameCounterRequest : EzspFrameRequest
{
    /// <summary>
    /// Whether or not a neighbor&apos;s incoming FC should be reset upon rejoining (true or false).
    /// </summary>
    public bool reset { get; set; }

}
