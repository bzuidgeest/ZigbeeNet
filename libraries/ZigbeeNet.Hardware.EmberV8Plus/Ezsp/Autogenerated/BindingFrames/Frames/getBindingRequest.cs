using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// Gets an entry from the binding table.
/// Frame value: 0x002C
/// </summary>
public class getBinding : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

}
