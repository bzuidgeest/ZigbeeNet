using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Causes the stack to leave the current network. This generates a &lt;i&gt;stackStatusHandler&lt;/i&gt; callback to indicate that the network is down. The radio will not be used until after sending a &lt;i&gt;formNetwork&lt;/i&gt; or &lt;i&gt;joinNetwork&lt;/i&gt; command.
/// Frame value: 0x0020
/// </summary>
public class LeaveNetworkRequest : EzspFrameRequest
{
    /// <summary>
    /// This parameter gives options when leave network
    /// </summary>
    public sl_zigbee_leave_network_option_t options { get; set; }

}
