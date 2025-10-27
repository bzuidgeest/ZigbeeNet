using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Form a new sleepy-to-sleepy network.  If the network is using security, the device must call sli_zigbee_stack_set_initial_security_state() first.
/// Frame value: 0x0119
/// </summary>
public class SleepyToSleepyNetworkStartRequest : EzspFrameRequest
{
    /// <summary>
    /// Specification of the new network.
    /// </summary>
    public sl_zigbee_network_parameters_t parameters { get; set; }

    /// <summary>
    /// Whether this device is initiating or joining the network.
    /// </summary>
    public bool initiator { get; set; }

}
