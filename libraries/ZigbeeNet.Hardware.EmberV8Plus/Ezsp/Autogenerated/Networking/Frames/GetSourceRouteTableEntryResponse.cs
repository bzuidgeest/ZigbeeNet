using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns information about a source route table entry
/// Frame value: 0x00C1
/// </summary>
public class GetSourceRouteTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if there is source route entry at
	/// &lt;i&gt;index&lt;/i&gt;. SL_STATUS_NOT_FOUND if there is no
	/// source route at &lt;i&gt;index&lt;/i&gt;.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The node ID of the destination in that entry.
    /// </summary>
    public sl_802154_short_addr_t destination { get; set; }

    /// <summary>
    /// The closer node index for this source route table entry
    /// </summary>
    public byte closerIndex { get; set; }

}
