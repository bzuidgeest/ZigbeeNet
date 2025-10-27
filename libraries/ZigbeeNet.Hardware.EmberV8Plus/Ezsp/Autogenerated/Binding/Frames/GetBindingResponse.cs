using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;

/// <summary>
/// Gets an entry from the binding table.
/// Frame value: 0x002C
/// </summary>
public class GetBindingResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The contents of the binding entry.
    /// </summary>
    public sl_zigbee_binding_table_entry_t value { get; set; }

}
