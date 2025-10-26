using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Callback fires when the duty cycle state has changed
/// Frame value: 0x004D
/// </summary>
public class dutyCycleHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The channel page whose duty cycle state has changed.
    /// </summary>
    public byte channelPage { get; set; }

    /// <summary>
    /// The channel number whose duty cycle state has changed.
    /// </summary>
    public byte channel { get; set; }

    /// <summary>
    /// The current duty cycle state.
    /// </summary>
    public sl_zigbee_duty_cycle_state_t state { get; set; }

    /// <summary>
    /// The total number of connected end devices that are being monitored for duty cycle.
    /// </summary>
    public byte totalDevices { get; set; }

    /// <summary>
    /// Consumed duty cycles of end devices that are being monitored. The first entry always be the local stack&apos;s nodeId, and thus the total aggregate duty cycle for the device.
    /// </summary>
    public sl_zigbee_per_device_duty_cycle_t arrayOfDeviceDutyCycles { get; set; }

}
