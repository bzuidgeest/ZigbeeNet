using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when a bootload message is received.
/// Frame value: 0x0092
/// </summary>
public class IncomingBootloadMessageHandlerRequest : EzspFrameRequest
{
}
