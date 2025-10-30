using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// A callback invoked by the ZigBee GP stack when a GPDF is received.
/// Frame value: 0x00C5
/// </summary>
public class GpepIncomingMessageHandlerRequest : EzspFrameRequest
{
