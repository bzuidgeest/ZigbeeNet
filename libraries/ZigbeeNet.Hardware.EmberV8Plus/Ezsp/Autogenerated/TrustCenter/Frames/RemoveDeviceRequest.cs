using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// This command sends an APS remove device using APS encryption to the destination indicating either to remove itself from the network, or one of its children.
/// Frame value: 0x00A8
/// </summary>
public class RemoveDeviceRequest : EzspFrameRequest
{
    /// <summary>
    /// The node ID of the device that will receive the message
    /// </summary>
    public sl_802154_short_addr_t destShort { get; set; }

    /// <summary>
    /// The long address (EUI64) of the device that will receive the message.
    /// </summary>
    public sl_802154_long_addr_t destLong { get; set; }

    /// <summary>
    /// The long address (EUI64) of the device to be removed.
    /// </summary>
    public sl_802154_long_addr_t targetLong { get; set; }

}
