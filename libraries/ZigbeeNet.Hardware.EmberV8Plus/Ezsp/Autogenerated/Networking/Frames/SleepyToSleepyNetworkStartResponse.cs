using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Form a new sleepy-to-sleepy network.  If the network is using security, the device must call sli_zigbee_stack_set_initial_security_state() first.
/// Frame value: 0x0119
/// </summary>
public class SleepyToSleepyNetworkStartResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or a reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
