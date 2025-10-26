using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Check if a particular counter can report on the destination node ID they have been triggered from.
/// Frame value: 0x0133
/// </summary>
public class counterRequiresDestinationNodeIdResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether this counter requires the destination node ID.
    /// </summary>
    public bool requires { get; set; }

}
