using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Periodically request any pending data from our parent. Setting &lt;i&gt;interval&lt;/i&gt; to 0 or &lt;i&gt;units&lt;/i&gt; to SL_ZIGBEE_EVENT_INACTIVE will generate a single poll.
/// Frame value: 0x0042
/// </summary>
public class PollForDataRequest : EzspFrameRequest
{
    /// <summary>
    /// The time between polls. Note that the timer clock is free running and is not synchronized with this command. This means that the time will be between &lt;i&gt;interval&lt;/i&gt; and (&lt;i&gt;interval&lt;/i&gt; - 1). The maximum interval is 32767.
    /// </summary>
    public ushort interval { get; set; }

    /// <summary>
    /// The units for &lt;i&gt;interval&lt;/i&gt;.
    /// </summary>
    public sl_zigbee_event_units_t units { get; set; }

    /// <summary>
    /// The number of poll failures that will be tolerated before a &lt;i&gt;pollCompleteHandler&lt;/i&gt; callback is generated. A value of zero will result in a callback for every poll. Any status value apart from SL_STATUS_OK and SL_STATUS_MAC_NO_DATA is counted as a failure.
    /// </summary>
    public byte failureLimit { get; set; }

}
