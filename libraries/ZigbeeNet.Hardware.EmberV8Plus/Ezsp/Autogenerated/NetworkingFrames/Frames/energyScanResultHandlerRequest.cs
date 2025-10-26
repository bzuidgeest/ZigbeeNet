using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Reports the result of an energy scan for a single channel. The scan is not complete until the &lt;i&gt;scanCompleteHandler&lt;/i&gt; callback is called.
/// Frame value: 0x0048
/// </summary>
public class energyScanResultHandler : EzspFrameRequest
{
}
