using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Stops transmitting tone started by mfglibInternalStartTone().
/// Frame value: 0x0086
/// </summary>
public class mfglibInternalStopTone : EzspFrameRequest
{
}
