using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This causes to initialize the desired radio interface other than native and form a new network by becoming the coordinator with same panId as native radio network.
/// Frame value: 0x00F8
/// </summary>
public class MultiPhyStartResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
