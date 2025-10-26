using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Used to test that UART flow control is working correctly.
/// Frame value: 0x009D
/// </summary>
public class delayTestResponse : EzspFrameResponse
{
}
