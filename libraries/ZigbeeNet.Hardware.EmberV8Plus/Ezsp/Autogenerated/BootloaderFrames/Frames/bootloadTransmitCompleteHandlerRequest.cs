using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Command;

/// <summary>
/// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a bootload message.
/// Frame value: 0x0093
/// </summary>
public class bootloadTransmitCompleteHandler : EzspFrameRequest
{
}
