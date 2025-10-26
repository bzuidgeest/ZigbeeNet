using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Supply a source route for the next outgoing message.
/// Frame value: 0x00AE
/// </summary>
public class setSourceRouteResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the source route was successfully stored, and SL_STATUS_ALLOCATION_FAILED otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

}
