using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// A consolidation of ZLL network operations with similar signatures; specifically, forming and joining networks or touch-linking.
/// Frame value: 0x00B2
/// </summary>
public class ZllNetworkOpsResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
