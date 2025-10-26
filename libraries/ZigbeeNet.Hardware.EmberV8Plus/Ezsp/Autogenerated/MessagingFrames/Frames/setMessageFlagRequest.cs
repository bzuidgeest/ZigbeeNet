using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Set a flag to indicate that a message is pending for a child. The next time that the child polls, it will be informed that it has a pending message. The message is sent from emberPollHandler, which is called when the child requests data.
/// Frame value: 0x0136
/// </summary>
public class setMessageFlag : EzspFrameRequest
{
    /// <summary>
    /// The ID of the child that just polled for data.
    /// </summary>
    public sl_802154_short_addr_t childId { get; set; }

}
