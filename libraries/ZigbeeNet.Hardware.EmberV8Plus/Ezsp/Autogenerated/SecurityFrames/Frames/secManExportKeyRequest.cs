using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Exports a key from security manager based on passed context.
/// Frame value: 0x0114
/// </summary>
public class secManExportKey : EzspFrameRequest
{
    /// <summary>
    /// Metadata to identify the requested key.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

}
