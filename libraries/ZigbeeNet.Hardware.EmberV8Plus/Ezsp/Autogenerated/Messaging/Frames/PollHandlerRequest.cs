using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Indicates that the local node received a data poll from a child.
/// Frame value: 0x0044
/// </summary>
public class PollHandlerRequest : EzspFrameRequest
{
