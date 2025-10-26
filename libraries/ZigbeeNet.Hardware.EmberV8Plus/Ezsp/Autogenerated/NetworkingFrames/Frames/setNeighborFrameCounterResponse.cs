using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets the frame counter for the neighbour or child.
/// Frame value: 0x00AD
/// </summary>
public class setNeighborFrameCounterResponse : EzspFrameResponse
{
    /// <summary>
    /// Return SL_STATUS_NOT_FOUND if the node is not found in the neighbor or child table. Returns SL_STATUS_OK otherwise
    /// </summary>
    public sl_status_t status { get; set; }

}
