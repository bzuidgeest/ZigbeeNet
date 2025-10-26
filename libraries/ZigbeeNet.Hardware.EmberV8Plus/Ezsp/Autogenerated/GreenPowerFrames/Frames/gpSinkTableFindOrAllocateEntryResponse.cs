using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// Finds or allocates a sink entry
/// Frame value: 0x00E1
/// </summary>
public class gpSinkTableFindOrAllocateEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// An index of found or allocated sink or 0xFF if failed.
    /// </summary>
    public byte index { get; set; }

}
