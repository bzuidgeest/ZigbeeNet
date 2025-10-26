using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Indicate whether there are pending messages in the APS retry queue.
/// Frame value: 0x0121
/// </summary>
public class pendingAckedMessagesResponse : EzspFrameResponse
{
    /// <summary>
    /// True if there is a pending message for this network in the APS retry queue, false if not.
    /// </summary>
    public bool pending_messages { get; set; }

}
