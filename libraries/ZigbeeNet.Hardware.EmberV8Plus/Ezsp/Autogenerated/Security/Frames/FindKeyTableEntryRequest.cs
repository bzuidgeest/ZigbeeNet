using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function searches through the Key Table and tries to find the entry that matches the passed search criteria.
/// Frame value: 0x0075
/// </summary>
public class FindKeyTableEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The address to search for. Alternatively, all zeros may be passed in to search for the first empty entry.
    /// </summary>
    public sl_802154_long_addr_t address { get; set; }

    /// <summary>
    /// This indicates whether to search for an entry that contains a link key or a master key. true means to search for an entry with a Link Key.
    /// </summary>
    public bool linkKey { get; set; }

}
