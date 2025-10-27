using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sets the radio output power for desired phy interface at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this api on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
/// Frame value: 0x00FA
/// </summary>
public class MultiPhySetRadioPowerResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
