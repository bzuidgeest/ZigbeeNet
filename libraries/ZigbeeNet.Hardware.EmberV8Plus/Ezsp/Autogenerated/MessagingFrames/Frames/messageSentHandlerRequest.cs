using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// A callback indicating the stack has completed sending a message.
/// Frame value: 0x003F
/// </summary>
public class messageSentHandler : EzspFrameRequest
{
}
