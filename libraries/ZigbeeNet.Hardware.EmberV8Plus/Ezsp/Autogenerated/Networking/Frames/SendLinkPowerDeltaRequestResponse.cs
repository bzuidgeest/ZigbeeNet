using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Send Link Power Delta Request from a child to its parent
/// Frame value: 0x00F7
/// </summary>
public class SendLinkPowerDeltaRequestResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of sending the request.
    /// </summary>
    public sl_status_t status { get; set; }

}
