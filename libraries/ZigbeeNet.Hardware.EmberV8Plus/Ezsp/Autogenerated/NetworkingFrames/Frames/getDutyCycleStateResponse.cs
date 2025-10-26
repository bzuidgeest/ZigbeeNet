using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Obtains the current duty cycle state.
/// Frame value: 0x0035
/// </summary>
public class getDutyCycleStateResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The current duty cycle state in effect.
    /// </summary>
    public sl_zigbee_duty_cycle_state_t returnedState { get; set; }

}
