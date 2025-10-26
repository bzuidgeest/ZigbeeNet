using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Structure;

/// <summary>
/// Sets an entry in the binding table.
/// Frame value: 0x002B
/// </summary>
public class setBindingResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
