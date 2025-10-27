using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Reschedule sending link status message, with first one being sent immediately.
/// Frame value: 0x011B
/// </summary>
public class RescheduleLinkStatusMsgResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
