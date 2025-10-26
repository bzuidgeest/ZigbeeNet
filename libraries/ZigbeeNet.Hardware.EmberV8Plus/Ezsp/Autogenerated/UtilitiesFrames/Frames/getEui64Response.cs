using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Returns the EUI64 ID of the local node.
/// Frame value: 0x0026
/// </summary>
public class getEui64Response : EzspFrameResponse
{
    /// <summary>
    /// The 64-bit ID.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

}
