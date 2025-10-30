using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Clear a flag to indicate that there are no more messages for a child. The next time the child polls, it will be informed that it does not have any pending messages.
/// Frame value: 0x0137
/// </summary>
public class ClearMessageFlagRequest : EzspFrameRequest
{
    /// <summary>
    /// The ID of the child that no longer has pending messages.
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

