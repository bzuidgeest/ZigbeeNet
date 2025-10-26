using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Sets a timer on the NCP. There are 2 independent timers available for use by the Host. A timer can be cancelled by setting &lt;i&gt;time&lt;/i&gt; to 0 or &lt;i&gt;units&lt;/i&gt; to SL_ZIGBEE_EVENT_INACTIVE.
/// Frame value: 0x000E
/// </summary>
public class setTimerResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
