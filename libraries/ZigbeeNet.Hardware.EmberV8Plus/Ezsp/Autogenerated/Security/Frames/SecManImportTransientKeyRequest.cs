using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Import a transient link key.
/// Frame value: 0x0111
/// </summary>
public class SecManImportTransientKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// EUI64 associated with this transient key.
    /// </summary>
    public sl_802154_long_addr_t eui64 { get; set; }

    /// <summary>
    /// The key to import.
    /// </summary>
    public sl_zigbee_sec_man_key_t plaintext_key { get; set; }

}
