using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// A callback to the GP endpoint to indicate the result of the GPDF transmission.
/// Frame value: 0x00C7
/// </summary>
public class dGpSentHandler : EzspFrameRequest
{
}
