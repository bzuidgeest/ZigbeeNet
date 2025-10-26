using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Sets an entry in the multicast table.
/// Frame value: 0x0064
/// </summary>
public class setMulticastTableEntry : EzspFrameRequest
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
