using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Removes the proxy table entry stored at the passed index.
/// Frame value: 0x005D
/// </summary>
public class GpProxyTableRemoveEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested proxy table entry.
    /// </summary>
    public byte proxyIndex { get; set; }

}
