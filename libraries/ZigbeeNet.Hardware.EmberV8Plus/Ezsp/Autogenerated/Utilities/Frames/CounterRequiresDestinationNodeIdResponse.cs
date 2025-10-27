using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Check if a particular counter can report on the destination node ID they have been triggered from.
/// Frame value: 0x0133
/// </summary>
public class CounterRequiresDestinationNodeIdResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether this counter requires the destination node ID.
    /// </summary>
    public bool requires { get; set; }

}
