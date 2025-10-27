using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Reads a value from the NCP but passes an extra argument specific to the value being retrieved.
/// Frame value: 0x0003
/// </summary>
public class GetExtendedValueRequest : EzspFrameRequest
{
    /// <summary>
    /// Identifies which extended value ID to read.
    /// </summary>
    public sl_zigbee_ezsp_extended_value_id_t valueId { get; set; }

    /// <summary>
    /// Identifies which characteristics of the extended value ID to read. These are specific to the value being read.
    /// </summary>
    public uint characteristics { get; set; }

}
