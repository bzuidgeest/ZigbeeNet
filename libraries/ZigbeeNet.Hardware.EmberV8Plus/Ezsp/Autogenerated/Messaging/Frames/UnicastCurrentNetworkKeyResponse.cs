using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Send the network key to a destination.
/// Frame value: 0x0050
/// </summary>
public class UnicastCurrentNetworkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if send was successful
    /// </summary>
    public sl_status_t status { get; set; }

}
