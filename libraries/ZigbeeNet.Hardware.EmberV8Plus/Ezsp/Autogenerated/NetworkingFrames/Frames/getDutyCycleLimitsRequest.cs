using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Obtains the current duty cycle limits that were previously set by a call to sli_zigbee_stack_set_duty_cycle_limits_in_stack(), or the defaults set by the stack if no set call was made.
/// Frame value: 0x004B
/// </summary>
public class getDutyCycleLimits : EzspFrameRequest
{
}
