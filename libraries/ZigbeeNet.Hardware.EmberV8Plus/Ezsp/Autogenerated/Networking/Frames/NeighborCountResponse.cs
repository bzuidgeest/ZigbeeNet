using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the number of active entries in the neighbor table.
/// Frame value: 0x007A
/// </summary>
public class NeighborCountResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of active entries in the neighbor table.
    /// </summary>
    public byte value { get; set; }

}
