using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns information about a child of the local node.
/// Frame value: 0x004A
/// </summary>
public class getChildDataResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if there is a child at &lt;i&gt;index&lt;/i&gt;. SL_STATUS_NOT_JOINED if there is no child at &lt;i&gt;index&lt;/i&gt;.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The data of the child.
    /// </summary>
    public sl_zigbee_child_data_t childData { get; set; }

}
