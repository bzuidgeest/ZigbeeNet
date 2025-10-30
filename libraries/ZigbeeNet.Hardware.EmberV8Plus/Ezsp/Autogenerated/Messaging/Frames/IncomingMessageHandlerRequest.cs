using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback indicating a message has been received.
/// Frame value: 0x0045
/// </summary>
public class IncomingMessageHandlerRequest : EzspFrameRequest
{
