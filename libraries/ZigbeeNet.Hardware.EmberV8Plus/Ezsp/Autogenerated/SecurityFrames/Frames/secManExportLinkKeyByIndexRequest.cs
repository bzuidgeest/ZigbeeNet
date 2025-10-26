using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Export the link key at given index from the key table.
/// Frame value: 0x010F
/// </summary>
public class secManExportLinkKeyByIndex : EzspFrameRequest
{
    /// <summary>
    /// Index of key to export.
    /// </summary>
    public byte index { get; set; }

}
