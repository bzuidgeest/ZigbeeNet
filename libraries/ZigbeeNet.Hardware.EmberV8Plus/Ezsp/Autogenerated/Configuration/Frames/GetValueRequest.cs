using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Reads a value from the NCP.
/// Frame value: 0x00AA
/// </summary>
public class GetValueRequest : EzspFrameRequest
{
    /// <summary>
    /// Identifies which value to read.
    /// </summary>
    public sl_zigbee_ezsp_value_id_t valueId { get; set; }

}
