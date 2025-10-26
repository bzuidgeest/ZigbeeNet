using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// Sets an entry in the binding table.
/// Frame value: 0x002B
/// </summary>
public class setBinding : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// The contents of the binding entry.
    /// </summary>
    public sl_zigbee_binding_table_entry_t value { get; set; }

}
