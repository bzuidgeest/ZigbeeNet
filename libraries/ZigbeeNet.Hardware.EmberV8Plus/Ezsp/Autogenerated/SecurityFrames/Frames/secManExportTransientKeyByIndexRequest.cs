using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Export a transient link key from a given table index.
/// Frame value: 0x0112
/// </summary>
public class secManExportTransientKeyByIndex : EzspFrameRequest
{
    /// <summary>
    /// Index to export from.
    /// </summary>
    public byte index { get; set; }

}
