using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Set the current duty cycle limits configuration. The Default limits set by stack if this call is not made.
/// Frame value: 0x0040
/// </summary>
public class SetDutyCycleLimitsInStackResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK  if the duty cycle limit configurations set successfully, SL_STATUS_INVALID_PARAMETER if set illegal value such as setting only one of the limits to default or violates constraints Susp &gt; Crit &gt; Limi, SL_STATUS_INVALID_STATE if device is operating on 2.4Ghz
    /// </summary>
    public sl_status_t status { get; set; }

}
