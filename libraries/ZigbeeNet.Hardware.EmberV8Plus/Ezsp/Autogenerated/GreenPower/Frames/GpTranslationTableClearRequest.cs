using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Clears all entries within the translation table.
/// Frame value: 0x010B
/// </summary>
public class GpTranslationTableClearRequest : EzspFrameRequest
{
