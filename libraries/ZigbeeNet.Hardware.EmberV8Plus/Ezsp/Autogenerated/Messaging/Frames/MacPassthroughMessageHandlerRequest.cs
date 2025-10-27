using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when a MAC passthrough message is received.
/// Frame value: 0x0097
/// </summary>
public class MacPassthroughMessageHandlerRequest : EzspFrameRequest
{
}
