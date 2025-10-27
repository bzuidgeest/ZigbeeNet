using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns a value indicating whether the node is joining, joined to, or leaving a network.
/// Frame value: 0x0018
/// </summary>
public class NetworkStateResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_zigbee_network_status_t value indicating the current join status.
    /// </summary>
    public sl_zigbee_network_status_t status { get; set; }

}
