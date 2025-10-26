using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Check whether a key context can be used to load a valid key.
/// Frame value: 0x0110
/// </summary>
public class secManCheckKeyContextResponse : EzspFrameResponse
{
    /// <summary>
    /// Validity of the checked context.
    /// </summary>
    public sl_status_t status { get; set; }

}
