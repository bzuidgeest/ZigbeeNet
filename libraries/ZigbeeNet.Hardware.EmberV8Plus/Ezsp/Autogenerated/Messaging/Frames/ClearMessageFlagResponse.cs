using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Clear a flag to indicate that there are no more messages for a child. The next time the child polls, it will be informed that it does not have any pending messages.
/// Frame value: 0x0137
/// </summary>
public class ClearMessageFlagResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK - The next time that the child polls, it will be informed that it does not have any pending messages. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.
    /// </summary>
    public sl_status_t status { get; set; }

}
