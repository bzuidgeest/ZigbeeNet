using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// This call sets additional capability bits in the ZLL state.
/// Frame value: 0x00D6
/// </summary>
public class setZllAdditionalState : EzspFrameRequest
{
    /// <summary>
    /// A mask with the bits to be set or cleared.
    /// </summary>
    public ushort state { get; set; }

}
