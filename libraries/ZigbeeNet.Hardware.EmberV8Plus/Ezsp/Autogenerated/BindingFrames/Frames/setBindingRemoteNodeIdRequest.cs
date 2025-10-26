using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// Set the node ID for the binding&apos;s destination. See &lt;i&gt;getBindingRemoteNodeId&lt;/i&gt; for a description.
/// Frame value: 0x0030
/// </summary>
public class setBindingRemoteNodeId : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// The short ID of the destination node.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

}
