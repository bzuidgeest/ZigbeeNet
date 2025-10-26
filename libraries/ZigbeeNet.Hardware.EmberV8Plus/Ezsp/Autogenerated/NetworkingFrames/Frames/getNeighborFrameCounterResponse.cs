using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Return sl_status_t depending on whether the frame counter of the node is found in the neighbor or child table. This function gets the last received frame counter as found in the Network Auxiliary header for the specified neighbor or child
/// Frame value: 0x003E
/// </summary>
public class getNeighborFrameCounterResponse : EzspFrameResponse
{
    /// <summary>
    /// Return SL_STATUS_NOT_FOUND if the node is not found in the neighbor or child table. Returns SL_STATUS_OK otherwise
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Return the frame counter of the node from the neighbor or child table
    /// </summary>
    public uint returnFrameCounter { get; set; }

}
