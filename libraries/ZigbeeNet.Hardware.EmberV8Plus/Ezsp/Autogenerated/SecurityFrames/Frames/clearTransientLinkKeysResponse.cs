using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Clear all of the transient link keys from RAM.
/// Frame value: 0x006B
/// </summary>
public class clearTransientLinkKeysResponse : EzspFrameResponse
{
}
