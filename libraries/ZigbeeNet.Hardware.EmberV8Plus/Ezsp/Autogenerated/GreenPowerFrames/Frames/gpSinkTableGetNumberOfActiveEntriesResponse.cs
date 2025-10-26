using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// Return number of active entries in sink table.
/// Frame value: 0x0118
/// </summary>
public class gpSinkTableGetNumberOfActiveEntriesResponse : EzspFrameResponse
{
    /// <summary>
    /// Number of active entries in sink table.
    /// </summary>
    public byte number_of_entries { get; set; }

}
