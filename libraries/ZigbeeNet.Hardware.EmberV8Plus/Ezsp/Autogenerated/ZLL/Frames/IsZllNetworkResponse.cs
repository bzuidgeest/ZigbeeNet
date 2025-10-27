using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// Is this a ZLL network?
/// Frame value: 0x00BE
/// </summary>
public class IsZllNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// ZLL network?
    /// </summary>
    public bool isZllNetwork { get; set; }

}
