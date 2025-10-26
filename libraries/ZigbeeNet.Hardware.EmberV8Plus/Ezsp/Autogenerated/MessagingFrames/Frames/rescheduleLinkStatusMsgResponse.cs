using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Reschedule sending link status message, with first one being sent immediately.
/// Frame value: 0x011B
/// </summary>
public class rescheduleLinkStatusMsgResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
