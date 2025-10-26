using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Reads a configuration value from the NCP.
/// Frame value: 0x0052
/// </summary>
public class getConfigurationValueResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize &lt;i&gt;configId&lt;/i&gt;.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The configuration value.
    /// </summary>
    public ushort value { get; set; }

}
