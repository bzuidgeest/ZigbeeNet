using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Returns the entropy source used for true random number generation.
/// Frame value: 0x004F
/// </summary>
public class getTrueRandomEntropySourceResponse : EzspFrameResponse
{
    /// <summary>
    /// Value indicates the used entropy source.
    /// </summary>
    public sl_zigbee_entropy_source_t entropySource { get; set; }

}
