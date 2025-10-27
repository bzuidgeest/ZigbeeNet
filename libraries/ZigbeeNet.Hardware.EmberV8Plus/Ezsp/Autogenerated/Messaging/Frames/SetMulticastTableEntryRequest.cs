using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sets an entry in the multicast table.
/// Frame value: 0x0064
/// </summary>
public class SetMulticastTableEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of a multicast table entry
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// The contents of the multicast entry.
    /// </summary>
    public sl_zigbee_multicast_table_entry_t value { get; set; }

}
