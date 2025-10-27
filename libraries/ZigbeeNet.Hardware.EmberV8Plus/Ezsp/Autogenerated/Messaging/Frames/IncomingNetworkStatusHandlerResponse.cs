using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked when a network status/route error message is received. The error indicates that there was a problem sending/receiving messages from the target node
/// Frame value: 0x00C4
/// </summary>
public class IncomingNetworkStatusHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// One byte over-the-air error code from network status message
    /// </summary>
    public byte errorCode { get; set; }

    /// <summary>
    /// The short ID of the remote node
    /// </summary>
    public sl_802154_short_addr_t target { get; set; }

}
