using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Configures endpoint information on the NCP. The NCP does not remember these settings after a reset. Endpoints can be added by the Host after the NCP has reset. Once the status of the stack changes to SL_STATUS_NETWORK_UP, endpoints can no longer be added and this command will respond with SL_ZIGBEE_EZSP_ERROR_INVALID_CALL.
/// Frame value: 0x0002
/// </summary>
public class AddEndpointResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the endpoint was added, SL_STATUS_ZIGBEE_EZSP_ERROR if there was an error. Errors could be SL_ZIGBEE_EZSP_ERROR_OUT_OF_MEMORY if there is not enough memory available to add the endpoint, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the endpoint already exists, SL_ZIGBEE_EZSP_ERROR_INVALID_CALL if endpoints can no longer be added.
    /// </summary>
    public sl_status_t status { get; set; }

}
