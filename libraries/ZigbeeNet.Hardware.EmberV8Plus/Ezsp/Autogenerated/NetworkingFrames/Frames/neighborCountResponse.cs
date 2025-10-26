using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the number of active entries in the neighbor table.
/// Frame value: 0x007A
/// </summary>
public class neighborCountResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of active entries in the neighbor table.
    /// </summary>
    public byte value { get; set; }

}
