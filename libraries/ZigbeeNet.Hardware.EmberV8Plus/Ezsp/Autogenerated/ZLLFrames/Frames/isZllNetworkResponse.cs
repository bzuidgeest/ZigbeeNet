using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Is this a ZLL network?
/// Frame value: 0x00BE
/// </summary>
public class isZllNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// ZLL network?
    /// </summary>
    public bool isZllNetwork { get; set; }

}
