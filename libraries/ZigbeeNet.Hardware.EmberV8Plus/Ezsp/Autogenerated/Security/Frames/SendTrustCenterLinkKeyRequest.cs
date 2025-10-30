using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function sends an APS TransportKey command containing the current trust center link key. The node to which the command is sent is specified via the short and long address arguments.
/// Frame value: 0x0067
/// </summary>
public class SendTrustCenterLinkKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// The short address of the node to which this command will be sent
    /// </summary>
    public sl_802154_short_addr_t destinationNodeId { get; set; }

    /// <summary>
    /// The long address of the node to which this command will be sent
    /// </summary>
    public sl_802154_long_addr_t destinationEui64 { get; set; }

