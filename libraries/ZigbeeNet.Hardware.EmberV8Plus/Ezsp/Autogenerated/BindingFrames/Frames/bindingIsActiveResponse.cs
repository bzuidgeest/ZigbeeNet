using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Structure;

/// <summary>
/// Indicates whether any messages are currently being sent using this binding table entry. Note that this command does not indicate whether a binding is clear. To determine whether a binding is clear, check whether the type field of the sl_zigbee_binding_table_entry_t has the value SL_ZIGBEE_UNUSED_BINDING.
/// Frame value: 0x002E
/// </summary>
public class bindingIsActiveResponse : EzspFrameResponse
{
    /// <summary>
    /// True if the binding table entry is active, false otherwise.
    /// </summary>
    public bool active { get; set; }

}
