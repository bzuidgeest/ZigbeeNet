using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Starts transmitting a random stream of characters. This is so that the radio modulation can be measured.
/// Frame value: 0x0087
/// </summary>
public class mfglibInternalStartStream : EzspFrameRequest
{
}
