using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Sets the security state that will be used by the device when it forms or joins the network. This call &lt;b&gt;should not&lt;/b&gt; be used when restoring saved network state via networkInit as this will result in a loss of security data and will cause communication problems when the device re-enters the network.
/// Frame value: 0x0068
/// </summary>
public class setInitialSecurityStateResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure code of the operation.
    /// </summary>
    public sl_status_t success { get; set; }

}
