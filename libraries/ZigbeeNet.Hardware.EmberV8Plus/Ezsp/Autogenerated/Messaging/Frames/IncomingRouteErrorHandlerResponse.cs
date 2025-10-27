using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked when a route error message is received. The error indicates that a problem routing to or from the target node was encountered.
/// Frame value: 0x0080
/// </summary>
public class IncomingRouteErrorHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_ZIGBEE_SOURCE_ROUTE_FAILURE or SL_STATUS_ZIGBEE_MANY_TO_ONE_ROUTE_FAILURE.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The short id of the remote node.
    /// </summary>
    public sl_802154_short_addr_t target { get; set; }

}
