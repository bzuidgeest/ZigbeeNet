using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Check whether a key context can be used to load a valid key.
/// Frame value: 0x0110
/// </summary>
public class secManCheckKeyContext : EzspFrameRequest
{
    /// <summary>
    /// Context struct to check the validity of.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

}
