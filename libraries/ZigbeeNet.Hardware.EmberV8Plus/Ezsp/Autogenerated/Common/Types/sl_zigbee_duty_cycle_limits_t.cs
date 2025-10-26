namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// A structure containing duty cycle limit configurations. All limits are absolute, and are required to be as follows: suspLimit > critThresh > limitThresh For example:  suspLimit = 250 (2.5%), critThresh = 180 (1.8%), limitThresh 100 (1.00%).
/// </summary>
public struct sl_zigbee_duty_cycle_limits_t
{
    /// <summary>
    /// The Limited Threshold in % * 100
    /// </summary>
    public sl_zigbee_duty_cycle_hecto_pct_t limitThresh;

    /// <summary>
    /// The Critical Threshold in % * 100.
    /// </summary>
    public sl_zigbee_duty_cycle_hecto_pct_t critThresh;

    /// <summary>
    /// The Suspended Limit (LBT) in % * 100.
    /// </summary>
    public sl_zigbee_duty_cycle_hecto_pct_t suspLimit;

}

