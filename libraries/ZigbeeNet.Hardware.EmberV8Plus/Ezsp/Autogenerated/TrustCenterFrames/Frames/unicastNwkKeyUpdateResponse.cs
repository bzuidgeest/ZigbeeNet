using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenterFrames.Structure;

/// <summary>
/// This command will send a unicast transport key message with a new NWK key to the specified device. APS encryption using the device&apos;s existing link key will be used.
/// Frame value: 0x00A9
/// </summary>
public class unicastNwkKeyUpdateResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success, or the reason for failure
    /// </summary>
    public sl_status_t status { get; set; }

}
