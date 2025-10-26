using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Indicate whether the parent token has been set by association.
/// Frame value: 0x0140
/// </summary>
public class parentTokenSetResponse : EzspFrameResponse
{
    /// <summary>
    /// True if the parent token has been set.
    /// </summary>
    public bool indicator { get; set; }

}
