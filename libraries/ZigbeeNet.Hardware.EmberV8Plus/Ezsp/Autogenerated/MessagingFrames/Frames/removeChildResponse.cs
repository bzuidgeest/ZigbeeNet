using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Remove a node from child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely.
/// Frame value: 0x0139
/// </summary>
public class removeChildResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK - This node has been successfully removed. SL_STATUS_FAIL - The node was not found in either of the child or neighbor tables.
    /// </summary>
    public sl_status_t status { get; set; }

}
