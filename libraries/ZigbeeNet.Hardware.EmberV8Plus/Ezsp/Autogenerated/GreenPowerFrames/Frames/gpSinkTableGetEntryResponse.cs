using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// Retrieves the sink table entry stored at the passed index.
/// Frame value: 0x00DD
/// </summary>
public class gpSinkTableGetEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// An sl_zigbee_gp_sink_table_entry_t struct containing a copy of the requested sink entry.
    /// </summary>
    public sl_zigbee_gp_sink_table_entry_t entry { get; set; }

}
