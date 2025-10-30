using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when a raw MAC message that has matched one of the application&apos;s configured MAC filters.
/// Frame value: 0x0046
/// </summary>
public class MacFilterMatchMessageHandlerRequest : EzspFrameRequest
{
