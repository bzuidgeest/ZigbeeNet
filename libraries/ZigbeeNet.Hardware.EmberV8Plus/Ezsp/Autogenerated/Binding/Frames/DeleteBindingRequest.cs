using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;

/// <summary>
/// Deletes a binding table entry.
/// Frame value: 0x002D
/// </summary>
public class DeleteBindingRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

}
