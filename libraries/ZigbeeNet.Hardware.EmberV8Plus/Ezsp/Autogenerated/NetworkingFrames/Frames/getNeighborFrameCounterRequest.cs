using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Return sl_status_t depending on whether the frame counter of the node is found in the neighbor or child table. This function gets the last received frame counter as found in the Network Auxiliary header for the specified neighbor or child
/// Frame value: 0x003E
/// </summary>
public class getNeighborFrameCounter : EzspFrameRequest
{
    /// <summary>
    /// eui64 of the node
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

}
