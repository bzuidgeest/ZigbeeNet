using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Reschedule sending link status message, with first one being sent immediately.
/// Frame value: 0x011B
/// </summary>
public class rescheduleLinkStatusMsg : EzspFrameRequest
{
}
