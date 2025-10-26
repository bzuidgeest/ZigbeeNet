using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Reports that a network was found as a result of a prior call to startScan. Gives the network parameters useful for deciding which network to join.
/// Frame value: 0x001B
/// </summary>
public class networkFoundHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The parameters associated with the network found.
    /// </summary>
    public sl_zigbee_zigbee_network_t networkFound { get; set; }

    /// <summary>
    /// Link quality of incoming packet from network.
    /// </summary>
    public byte lastHopLqi { get; set; }

    /// <summary>
    /// Power (in dBm) of incoming packet.
    /// </summary>
    public sbyte lastHopRssi { get; set; }

}
