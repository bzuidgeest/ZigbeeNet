using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Sets security framecounter in the sink table
/// Frame value: 0x00F5
/// </summary>
public class gpSinkTableSetSecurityFrameCounter : EzspFrameRequest
{
    /// <summary>
    /// Index to the Sink table
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// Security Frame Counter
    /// </summary>
    public uint sfc { get; set; }

}
