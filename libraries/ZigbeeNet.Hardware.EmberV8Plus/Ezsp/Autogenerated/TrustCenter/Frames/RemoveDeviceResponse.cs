using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// This command sends an APS remove device using APS encryption to the destination indicating either to remove itself from the network, or one of its children.
/// Frame value: 0x00A8
/// </summary>
public class RemoveDeviceResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success, or the reason for failure
    /// </summary>
    public sl_status_t status { get; set; }

}
