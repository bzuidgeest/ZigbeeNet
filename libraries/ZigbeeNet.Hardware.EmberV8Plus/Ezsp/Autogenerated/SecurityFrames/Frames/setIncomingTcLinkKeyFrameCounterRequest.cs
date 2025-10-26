using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Set the incoming TC link key frame counter to desired value.
/// Frame value: 0x0128
/// </summary>
public class setIncomingTcLinkKeyFrameCounter : EzspFrameRequest
{
    /// <summary>
    /// Value to set the frame counter to.
    /// </summary>
    public uint frameCounter { get; set; }

}
