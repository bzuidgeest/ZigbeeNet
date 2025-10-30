using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Retrieves the sink table entry stored at the passed index.
/// Frame value: 0x00DF
/// </summary>
public class GpSinkTableSetEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested sink table entry.
    /// </summary>
    public byte sinkIndex { get; set; }

    /// <summary>
    /// An sl_zigbee_gp_sink_table_entry_t struct containing a copy of the sink entry to be updated.
    /// </summary>
    public sl_zigbee_gp_sink_table_entry_t entry { get; set; }

