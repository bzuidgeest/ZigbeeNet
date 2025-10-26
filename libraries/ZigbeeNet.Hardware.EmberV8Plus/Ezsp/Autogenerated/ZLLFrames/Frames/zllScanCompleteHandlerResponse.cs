using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// This call is fired when a ZLL network scan is complete.
/// Frame value: 0x00B7
/// </summary>
public class zllScanCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
