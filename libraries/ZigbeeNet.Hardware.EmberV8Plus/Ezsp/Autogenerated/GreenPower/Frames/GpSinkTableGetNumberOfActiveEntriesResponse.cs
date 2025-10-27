using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Return number of active entries in sink table.
/// Frame value: 0x0118
/// </summary>
public class GpSinkTableGetNumberOfActiveEntriesResponse : EzspFrameResponse
{
    /// <summary>
    /// Number of active entries in sink table.
    /// </summary>
    public byte number_of_entries { get; set; }

}
