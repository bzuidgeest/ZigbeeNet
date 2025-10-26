using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Is there a ZLL (Touchlink) operation in progress?
/// Frame value: 0x00D7
/// </summary>
public class zllOperationInProgressResponse : EzspFrameResponse
{
    /// <summary>
    /// ZLL operation in progress?
    /// </summary>
    public bool zllOperationInProgress { get; set; }

}
