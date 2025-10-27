namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Duty cycle states.
/// </summary>
public enum ZigbeeDutyCycleState : byte
{
    /// <summary>
    /// No Duty cycle tracking or metrics are taking place.
    /// </summary>
    SL_ZIGBEE_DUTY_CYCLE_TRACKING_OFF = 0,
    /// <summary>
    /// Duty Cycle is tracked and has not exceeded any thresholds.
    /// </summary>
    SL_ZIGBEE_DUTY_CYCLE_LBT_NORMAL = 1,
    /// <summary>
    /// We have exceeded the limited threshold of our total duty cycle allotment.
    /// </summary>
    SL_ZIGBEE_DUTY_CYCLE_LBT_LIMITED_THRESHOLD_REACHED = 2,
    /// <summary>
    /// We have exceeded the critical threshold of our total duty cycle allotment
    /// </summary>
    SL_ZIGBEE_DUTY_CYCLE_LBT_CRITICAL_THRESHOLD_REACHED = 3,
    /// <summary>
    /// We have reached the suspend limit and are blocking all outbound transmissions.
    /// </summary>
    SL_ZIGBEE_DUTY_CYCLE_LBT_SUSPEND_LIMIT_REACHED = 4
}
