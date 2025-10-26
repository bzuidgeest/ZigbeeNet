using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the radio output power at which a node is operating. Ember radios have discrete power settings. For a list of available power settings, see the technical specification for the RF communication module in your Developer Kit. Note: Care should be taken when using this API on a running network, as it will directly impact the established link qualities neighboring nodes have with the node on which it is called. This can lead to disruption of existing routes and erratic network behavior.
/// Frame value: 0x0099
/// </summary>
public class setRadioPower : EzspFrameRequest
{
    /// <summary>
    /// Desired radio output power, in dBm.
    /// </summary>
    public sbyte power { get; set; }

}
