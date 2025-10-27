using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Set a flag to indicate that a message is pending for a child. The next time that the child polls, it will be informed that it has a pending message. The message is sent from emberPollHandler, which is called when the child requests data.
/// Frame value: 0x0136
/// </summary>
public class SetMessageFlagResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK - The next time that the child polls, it will be informed that it has pending data. SL_STATUS_NOT_JOINED - The child identified by childId is not our child.
    /// </summary>
    public sl_status_t status { get; set; }

}
