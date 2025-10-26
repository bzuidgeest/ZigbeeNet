using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Import an application link key into the key table.
/// Frame value: 0x010E
/// </summary>
public class secManImportLinkKey : EzspFrameRequest
{
    /// <summary>
    /// Index where this key is to be imported to.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// EUI64 this key is associated with.
    /// </summary>
    public sl_802154_long_addr_t address { get; set; }

    /// <summary>
    /// The key data to be imported.
    /// </summary>
    public sl_zigbee_sec_man_key_t plaintext_key { get; set; }

}
