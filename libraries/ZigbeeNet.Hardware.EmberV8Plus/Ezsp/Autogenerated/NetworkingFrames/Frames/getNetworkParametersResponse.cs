using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the current network parameters.
/// Frame value: 0x0028
/// </summary>
public class getNetworkParametersResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// An sl_zigbee_node_type_t value indicating the current node type.
    /// </summary>
    public sl_zigbee_node_type_t nodeType { get; set; }

    /// <summary>
    /// The current network parameters.
    /// </summary>
    public sl_zigbee_network_parameters_t parameters { get; set; }

}
