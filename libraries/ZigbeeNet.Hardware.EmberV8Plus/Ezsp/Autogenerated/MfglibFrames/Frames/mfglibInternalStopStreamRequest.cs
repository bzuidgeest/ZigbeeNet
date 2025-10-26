using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Stops transmitting a random stream of characters started by mfglibInternalStartStream().
/// Frame value: 0x0088
/// </summary>
public class mfglibInternalStopStream : EzspFrameRequest
{
}
