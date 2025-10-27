using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Finds the index of the passed address in the gp table.
/// Frame value: 0x00C0
/// </summary>
public class GpProxyTableLookupResponse : EzspFrameResponse
{
    /// <summary>
    /// The index, or 0xFF for not found
    /// </summary>
    public byte index { get; set; }

}
