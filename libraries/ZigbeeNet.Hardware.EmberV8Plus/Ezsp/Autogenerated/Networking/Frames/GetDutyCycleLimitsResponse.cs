using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Obtains the current duty cycle limits that were previously set by a call to sli_zigbee_stack_set_duty_cycle_limits_in_stack(), or the defaults set by the stack if no set call was made.
/// Frame value: 0x004B
/// </summary>
public class GetDutyCycleLimitsResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Return current duty cycle limits if returnedLimits is not NULL
    /// </summary>
    public sl_zigbee_duty_cycle_limits_t returnedLimits { get; set; }

}
