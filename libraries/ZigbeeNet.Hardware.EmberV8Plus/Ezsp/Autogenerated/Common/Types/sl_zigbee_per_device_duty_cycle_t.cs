namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// A structure containing per device overall duty cycle consumed (up to the suspend limit).
/// </summary>
public struct sl_zigbee_per_device_duty_cycle_t
{
    /// <summary>
    /// Node Id of device whose duty cycle is reported.
    /// </summary>
    public sl_802154_short_addr_t nodeId;

    /// <summary>
    /// Amount of overall duty cycle consumed (up to suspend limit).
    /// </summary>
    public sl_zigbee_duty_cycle_hecto_pct_t dutyCycleConsumed;

}

