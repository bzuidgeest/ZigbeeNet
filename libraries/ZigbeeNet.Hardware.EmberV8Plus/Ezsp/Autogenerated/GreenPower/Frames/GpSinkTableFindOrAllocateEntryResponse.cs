using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Finds or allocates a sink entry
/// Frame value: 0x00E1
/// </summary>
public class GpSinkTableFindOrAllocateEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// An index of found or allocated sink or 0xFF if failed.
    /// </summary>
    public byte index { get; set; }

}
