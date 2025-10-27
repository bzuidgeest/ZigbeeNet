using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Indicates that the local node received a data poll from a child.
/// Frame value: 0x0044
/// </summary>
public class PollHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The node ID of the child that is requesting data.
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

    /// <summary>
    /// True if transmit is expected, false otherwise.
    /// </summary>
    public bool transmitExpected { get; set; }

}
