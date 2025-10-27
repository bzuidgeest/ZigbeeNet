using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Returns the entropy source used for true random number generation.
/// Frame value: 0x004F
/// </summary>
public class GetTrueRandomEntropySourceRequest : EzspFrameRequest
{
}
