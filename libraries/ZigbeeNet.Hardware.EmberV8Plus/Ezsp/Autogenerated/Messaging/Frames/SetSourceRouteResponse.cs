using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Supply a source route for the next outgoing message.
/// Frame value: 0x00AE
/// </summary>
public class SetSourceRouteResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the source route was successfully stored, and SL_STATUS_ALLOCATION_FAILED otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

}
