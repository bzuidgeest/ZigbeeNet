using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns the neighbor table entry at the given index. The number of active neighbors can be obtained using the neighborCount command.
/// Frame value: 0x0079
/// </summary>
public class getNeighbor : EzspFrameRequest
{
    /// <summary>
    /// The index of the neighbor of interest. Neighbors are stored in ascending order by node id, with all unused entries at the end of the table.
    /// </summary>
    public byte index { get; set; }

}
