using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Read attribute data on NCP endpoints.
/// Frame value: 0x0108
/// </summary>
public class readAttributeResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_zigbee_af_status_t value indicating success or the reason for failure, handled by the EZSP layer as a uint8_t. 255 indicates an EZSP-specific error.
    /// </summary>
    public sl_zigbee_af_status_t af_status { get; set; }

    /// <summary>
    /// Attribute data type.
    /// </summary>
    public byte dataType { get; set; }

    /// <summary>
    /// Length of attribute data.
    /// </summary>
    public byte readLength { get; set; }

    /// <summary>
    /// Attribute data.
    /// </summary>
    public uint8_t[readLength] dataPtr { get; set; }

}
