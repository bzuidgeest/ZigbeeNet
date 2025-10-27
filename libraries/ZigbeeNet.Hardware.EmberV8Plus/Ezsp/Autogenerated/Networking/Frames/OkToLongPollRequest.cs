using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate whether the stack is currently in a state that does not require the application to periodically poll.
/// Frame value: 0x0142
/// </summary>
public class OkToLongPollRequest : EzspFrameRequest
{
}
