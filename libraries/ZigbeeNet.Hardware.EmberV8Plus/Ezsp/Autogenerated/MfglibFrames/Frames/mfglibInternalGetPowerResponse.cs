using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Structure;

/// <summary>
/// Returns the current radio power setting, as previously set via mfglibInternalSetPower().
/// Frame value: 0x008d
/// </summary>
public class mfglibInternalGetPowerResponse : EzspFrameResponse
{
    /// <summary>
    /// Power in units of dBm. Refer to radio data sheet for valid range.
    /// </summary>
    public sbyte power { get; set; }

}
