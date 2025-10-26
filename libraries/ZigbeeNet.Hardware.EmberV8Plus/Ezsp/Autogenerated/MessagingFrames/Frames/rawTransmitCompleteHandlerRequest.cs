using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// A callback invoked by the EmberZNet stack when the MAC has finished transmitting a raw message.
/// Frame value: 0x0098
/// </summary>
public class rawTransmitCompleteHandler : EzspFrameRequest
{
}
