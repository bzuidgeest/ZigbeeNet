using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Gets information about a timer. The Host can use this command to find out how much longer it will be before a previously set timer will generate a callback.
/// Frame value: 0x004E
/// </summary>
public class getTimerResponse : EzspFrameResponse
{
    /// <summary>
    /// The delay before the &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated.
    /// </summary>
    public ushort time { get; set; }

    /// <summary>
    /// The units for &lt;i&gt;time&lt;/i&gt;.
    /// </summary>
    public sl_zigbee_event_units_t units { get; set; }

    /// <summary>
    /// True if a &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated repeatedly. False if only a single &lt;i&gt;timerHandler&lt;/i&gt; callback will be generated.
    /// </summary>
    public bool repeat { get; set; }

}
