using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Returns the entropy source used for true random number generation.
/// Frame value: 0x004F
/// </summary>
public class getTrueRandomEntropySource : EzspFrameRequest
{
}
