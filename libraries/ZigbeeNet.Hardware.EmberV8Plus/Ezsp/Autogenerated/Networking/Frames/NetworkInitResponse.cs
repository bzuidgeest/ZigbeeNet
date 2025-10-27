using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Resume network operation after a reboot. The node retains its original type. This should be called on startup whether or not the node was previously part of a network. SL_STATUS_NOT_JOINED is returned if the node is not part of a network. This command accepts options to control the network initialization.
/// Frame value: 0x0017
/// </summary>
public class NetworkInitResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value that indicates one of the following: successful initialization, SL_STATUS_NOT_JOINED if the node is not part of a network, or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
