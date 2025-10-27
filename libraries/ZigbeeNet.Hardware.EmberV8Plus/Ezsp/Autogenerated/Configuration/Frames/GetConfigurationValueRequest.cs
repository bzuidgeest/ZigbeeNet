using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Reads a configuration value from the NCP.
/// Frame value: 0x0052
/// </summary>
public class GetConfigurationValueRequest : EzspFrameRequest
{
    /// <summary>
    /// Identifies which configuration value to read.
    /// </summary>
    public sl_zigbee_ezsp_config_id_t configId { get; set; }

}
