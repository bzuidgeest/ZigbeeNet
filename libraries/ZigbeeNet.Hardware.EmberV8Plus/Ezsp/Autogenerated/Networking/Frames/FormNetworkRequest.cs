using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Forms a new network by becoming the coordinator.
/// Frame value: 0x001E
/// </summary>
public class FormNetworkRequest : EzspFrameRequest
{
    /// <summary>
    /// Specification of the new network.
    /// </summary>
    public sl_zigbee_network_parameters_t parameters { get; set; }

}
