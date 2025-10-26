using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Imports a key into security manager based on passed context.
/// Frame value: 0x0115
/// </summary>
public class secManImportKey : EzspFrameRequest
{
    /// <summary>
    /// Metadata to identify where the imported key should be stored.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

    /// <summary>
    /// The key to be imported.
    /// </summary>
    public sl_zigbee_sec_man_key_t key { get; set; }

}
