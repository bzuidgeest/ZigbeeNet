using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;

/// <summary>
/// Set the node ID for the binding&apos;s destination. See &lt;i&gt;getBindingRemoteNodeId&lt;/i&gt; for a description.
/// Frame value: 0x0030
/// </summary>
public class SetBindingRemoteNodeIdRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of a binding table entry.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// The short ID of the destination node.
    /// </summary>
    public sl_802154_short_addr_t nodeId { get; set; }

