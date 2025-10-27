using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// This command will send a unicast transport key message with a new NWK key to the specified device. APS encryption using the device&apos;s existing link key will be used.
/// Frame value: 0x00A9
/// </summary>
public class UnicastNwkKeyUpdateRequest : EzspFrameRequest
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
    /// The NWK key to send to the new device.
    /// </summary>
    public sl_zigbee_key_data_t key { get; set; }

}
