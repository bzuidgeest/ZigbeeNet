using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the neighbor table entry at the given index. The number of active neighbors can be obtained using the neighborCount command.
/// Frame value: 0x0079
/// </summary>
public class getNeighborResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_FAIL if the index is greater or equal to the number of active neighbors, or if the device is an end device. Returns SL_STATUS_OK otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The contents of the neighbor table entry.
    /// </summary>
    public sl_zigbee_neighbor_table_entry_t value { get; set; }

}
