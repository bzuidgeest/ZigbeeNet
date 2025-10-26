using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Causes the stack to leave the current network. This generates a &lt;i&gt;stackStatusHandler&lt;/i&gt; callback to indicate that the network is down. The radio will not be used until after sending a &lt;i&gt;formNetwork&lt;/i&gt; or &lt;i&gt;joinNetwork&lt;/i&gt; command.
/// Frame value: 0x0020
/// </summary>
public class leaveNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
