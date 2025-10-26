using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Reports the result of an energy scan for a single channel. The scan is not complete until the &lt;i&gt;scanCompleteHandler&lt;/i&gt; callback is called.
/// Frame value: 0x0048
/// </summary>
public class energyScanResultHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The 802.15.4 channel number that was scanned.
    /// </summary>
    public byte channel { get; set; }

    /// <summary>
    /// The maximum RSSI value found on the channel.
    /// </summary>
    public sbyte maxRssiValue { get; set; }

}
