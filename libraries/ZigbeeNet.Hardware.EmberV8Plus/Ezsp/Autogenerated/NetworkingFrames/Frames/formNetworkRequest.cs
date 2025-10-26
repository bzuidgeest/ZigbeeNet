using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Forms a new network by becoming the coordinator.
/// Frame value: 0x001E
/// </summary>
public class formNetwork : EzspFrameRequest
{
    /// <summary>
    /// Specification of the new network.
    /// </summary>
    public sl_zigbee_network_parameters_t parameters { get; set; }

}
