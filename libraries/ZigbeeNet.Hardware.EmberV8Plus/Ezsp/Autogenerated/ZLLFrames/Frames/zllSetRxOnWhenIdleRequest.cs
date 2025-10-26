using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// This call will change the mode of the radio so that the receiver is on for a specified amount of time when the device is idle.
/// Frame value: 0x00B5
/// </summary>
public class zllSetRxOnWhenIdle : EzspFrameRequest
{
    /// <summary>
    /// The duration in milliseconds to leave the radio on.
    /// </summary>
    public uint durationMs { get; set; }

}
