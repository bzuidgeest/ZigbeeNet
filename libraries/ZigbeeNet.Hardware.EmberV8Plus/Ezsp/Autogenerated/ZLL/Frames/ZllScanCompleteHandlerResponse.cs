using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when a ZLL network scan is complete.
/// Frame value: 0x00B7
/// </summary>
public class ZllScanCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
