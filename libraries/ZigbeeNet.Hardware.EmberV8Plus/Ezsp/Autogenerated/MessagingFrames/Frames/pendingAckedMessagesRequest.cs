using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Indicate whether there are pending messages in the APS retry queue.
/// Frame value: 0x0121
/// </summary>
public class pendingAckedMessages : EzspFrameRequest
{
}
