using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Returns the current radio power setting, as previously set via mfglibInternalSetPower().
/// Frame value: 0x008d
/// </summary>
public class MfglibInternalGetPowerResponse : EzspFrameResponse
{
    /// <summary>
    /// Power in units of dBm. Refer to radio data sheet for valid range.
    /// </summary>
    public sbyte power { get; set; }

}
