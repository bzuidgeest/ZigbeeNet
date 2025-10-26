using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets child data to the child table token.
/// Frame value: 0x00AC
/// </summary>
public class setChildDataResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the child data is set successfully at &lt;i&gt;index&lt;/i&gt;. SL_STATUS_INVALID_INDEX if provided &lt;i&gt;index&lt;/i&gt; is out of range.
    /// </summary>
    public sl_status_t status { get; set; }

}
