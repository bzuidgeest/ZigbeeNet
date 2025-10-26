using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// This call gets the radio&apos;s default idle power mode.
/// Frame value: 0x00BA
/// </summary>
public class zllGetRadioIdleModeResponse : EzspFrameResponse
{
    /// <summary>
    /// The current power mode.
    /// </summary>
    public byte radioIdleMode { get; set; }

}
