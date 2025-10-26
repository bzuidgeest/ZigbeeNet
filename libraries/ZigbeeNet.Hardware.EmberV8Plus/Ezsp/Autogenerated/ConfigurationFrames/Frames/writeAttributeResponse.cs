using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Write attribute data on NCP endpoints.
/// Frame value: 0x0109
/// </summary>
public class writeAttributeResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_zigbee_af_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_zigbee_af_status_t af_status { get; set; }

}
