using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// Deletes a binding table entry.
/// Frame value: 0x002D
/// </summary>
public class deleteBinding : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

}
