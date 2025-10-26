using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Gets an entry from the multicast table.
/// Frame value: 0x0063
/// </summary>
public class getMulticastTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The contents of the multicast entry.
    /// </summary>
    public sl_zigbee_multicast_table_entry_t value { get; set; }

}
