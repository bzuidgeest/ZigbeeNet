using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Clears all entries within the translation table.
/// Frame value: 0x010B
/// </summary>
public class gpTranslationTableClear : EzspFrameRequest
{
}
