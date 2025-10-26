using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Is the ZLL radio on when idle mode is active?
/// Frame value: 0x00D8
/// </summary>
public class zllRxOnWhenIdleGetActiveResponse : EzspFrameResponse
{
    /// <summary>
    /// ZLL radio on when idle mode is active?
    /// </summary>
    public bool zllRxOnWhenIdleGetActive { get; set; }

}
