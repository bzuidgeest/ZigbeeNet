using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Add a child to the child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely. If used appropriately, the application can maintain more than the maximum of children provided by the stack.
/// Frame value: 0x0138
/// </summary>
public class addChildResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK - This node has been successfully added. SL_STATUS_FAIL - The child was not added to the child/neighbor table.
    /// </summary>
    public sl_status_t status { get; set; }

}
