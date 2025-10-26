using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// Retrieves the proxy table entry stored at the passed index.
/// Frame value: 0x00C8
/// </summary>
public class gpProxyTableGetEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// An sl_zigbee_gp_proxy_table_entry_t struct containing a copy of the requested proxy entry.
    /// </summary>
    public sl_zigbee_gp_proxy_table_entry_t entry { get; set; }

}
