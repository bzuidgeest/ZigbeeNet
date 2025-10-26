using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// A callback invoked by the EmberZNet stack when a MAC passthrough message is received.
/// Frame value: 0x0097
/// </summary>
public class macPassthroughMessageHandler : EzspFrameRequest
{
}
