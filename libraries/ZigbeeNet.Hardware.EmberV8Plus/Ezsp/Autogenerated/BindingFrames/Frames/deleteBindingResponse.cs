using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Structure;

/// <summary>
/// Deletes a binding table entry.
/// Frame value: 0x002D
/// </summary>
public class deleteBindingResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
