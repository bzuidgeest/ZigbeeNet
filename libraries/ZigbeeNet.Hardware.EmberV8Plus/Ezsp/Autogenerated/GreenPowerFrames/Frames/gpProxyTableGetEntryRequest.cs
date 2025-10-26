using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Retrieves the proxy table entry stored at the passed index.
/// Frame value: 0x00C8
/// </summary>
public class gpProxyTableGetEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested proxy table entry.
    /// </summary>
    public byte proxyIndex { get; set; }

}
