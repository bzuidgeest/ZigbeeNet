using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Writes a value to the NCP.
/// Frame value: 0x00AB
/// </summary>
public class SetValueResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the value was changed, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the new value was out of bounds, SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;valueId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if the value could not be modified.
    /// </summary>
    public sl_status_t status { get; set; }

}
