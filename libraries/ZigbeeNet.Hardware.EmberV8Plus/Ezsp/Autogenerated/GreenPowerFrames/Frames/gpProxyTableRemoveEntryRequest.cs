using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Removes the proxy table entry stored at the passed index.
/// Frame value: 0x005D
/// </summary>
public class gpProxyTableRemoveEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested proxy table entry.
    /// </summary>
    public byte proxyIndex { get; set; }

}
