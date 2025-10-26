using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Writes a value to the NCP.
/// Frame value: 0x00AB
/// </summary>
public class setValue : EzspFrameRequest
{
    /// <summary>
    /// Identifies which value to change.
    /// </summary>
    public sl_zigbee_ezsp_value_id_t valueId { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;value&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte valueLength { get; set; }

    /// <summary>
    /// The new value.
    /// </summary>
    public uint8_t[valueLength] value { get; set; }

}
