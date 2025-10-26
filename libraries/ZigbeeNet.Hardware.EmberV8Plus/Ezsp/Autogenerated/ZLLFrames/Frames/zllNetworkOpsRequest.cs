using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// A consolidation of ZLL network operations with similar signatures; specifically, forming and joining networks or touch-linking.
/// Frame value: 0x00B2
/// </summary>
public class zllNetworkOps : EzspFrameRequest
{
    /// <summary>
    /// Information about the network.
    /// </summary>
    public sl_zigbee_zll_network_t networkInfo { get; set; }

    /// <summary>
    /// Operation indicator.
    /// </summary>
    public sl_zigbee_ezsp_zll_network_operation_t op { get; set; }

    /// <summary>
    /// Radio transmission power.
    /// </summary>
    public sbyte radioTxPower { get; set; }

}
