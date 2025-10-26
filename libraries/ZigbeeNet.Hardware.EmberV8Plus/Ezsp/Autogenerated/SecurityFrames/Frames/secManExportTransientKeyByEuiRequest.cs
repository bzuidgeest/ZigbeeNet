using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Export a transient link key associated with a given EUI64
/// Frame value: 0x0113
/// </summary>
public class secManExportTransientKeyByEui : EzspFrameRequest
{
    /// <summary>
    /// Index to export from.
    /// </summary>
    public sl_802154_long_addr_t eui { get; set; }

}
