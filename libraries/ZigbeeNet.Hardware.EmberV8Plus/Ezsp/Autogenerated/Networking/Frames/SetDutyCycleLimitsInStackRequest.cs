using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Set the current duty cycle limits configuration. The Default limits set by stack if this call is not made.
/// Frame value: 0x0040
/// </summary>
public class SetDutyCycleLimitsInStackRequest : EzspFrameRequest
{
    /// <summary>
    /// The duty cycle limits configuration to utilize.
    /// </summary>
    public sl_zigbee_duty_cycle_limits_t limits { get; set; }

