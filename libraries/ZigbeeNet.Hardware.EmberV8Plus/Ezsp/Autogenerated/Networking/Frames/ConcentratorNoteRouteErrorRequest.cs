using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Notes when a route error has occurred.
/// Frame value: 0x0151
/// </summary>
public class ConcentratorNoteRouteErrorRequest : EzspFrameRequest
{
    public sl_status_t status { get; set; }

    public sl_802154_short_addr_t nodeId { get; set; }

