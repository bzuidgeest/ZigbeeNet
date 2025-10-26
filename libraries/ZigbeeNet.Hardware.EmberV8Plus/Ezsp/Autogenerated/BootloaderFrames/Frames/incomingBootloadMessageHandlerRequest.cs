using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Command;

/// <summary>
/// A callback invoked by the EmberZNet stack when a bootload message is received.
/// Frame value: 0x0092
/// </summary>
public class incomingBootloadMessageHandler : EzspFrameRequest
{
}
