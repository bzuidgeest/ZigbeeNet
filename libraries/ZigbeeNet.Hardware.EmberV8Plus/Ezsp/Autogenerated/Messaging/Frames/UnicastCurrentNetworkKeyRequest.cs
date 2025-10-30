using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Send the network key to a destination.
/// Frame value: 0x0050
/// </summary>
public class UnicastCurrentNetworkKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// The destination node of the key.
    /// </summary>
    public sl_802154_short_addr_t targetShort { get; set; }

    /// <summary>
    /// The long address of the destination node.
    /// </summary>
    public sl_802154_long_addr_t targetLong { get; set; }

    /// <summary>
    /// The parent node of the destination node.
    /// </summary>
    public sl_802154_short_addr_t parentShortId { get; set; }

