using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Structure;

/// <summary>
/// Deletes all binding table entries.
/// Frame value: 0x002A
/// </summary>
public class clearBindingTableResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
