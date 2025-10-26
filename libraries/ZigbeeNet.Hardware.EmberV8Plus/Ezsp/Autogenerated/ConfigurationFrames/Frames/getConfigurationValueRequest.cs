using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Reads a configuration value from the NCP.
/// Frame value: 0x0052
/// </summary>
public class getConfigurationValue : EzspFrameRequest
{
    /// <summary>
    /// Identifies which configuration value to read.
    /// </summary>
    public sl_zigbee_ezsp_config_id_t configId { get; set; }

}
