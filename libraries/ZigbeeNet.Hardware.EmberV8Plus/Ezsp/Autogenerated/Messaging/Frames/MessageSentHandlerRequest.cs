using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback indicating the stack has completed sending a message.
/// Frame value: 0x003F
/// </summary>
public class MessageSentHandlerRequest : EzspFrameRequest
{
}
