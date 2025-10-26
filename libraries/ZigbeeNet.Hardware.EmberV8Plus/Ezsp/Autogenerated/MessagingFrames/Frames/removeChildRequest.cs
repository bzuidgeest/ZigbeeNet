using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Remove a node from child/neighbor table only on SoC, allowing direct manipulation of these tables by the application. This can affect the network functionality, and needs to be used wisely.
/// Frame value: 0x0139
/// </summary>
public class removeChild : EzspFrameRequest
{
    /// <summary>
    /// The long ID of the node.
    /// </summary>
    public sl_802154_long_addr_t childEui64 { get; set; }

}
