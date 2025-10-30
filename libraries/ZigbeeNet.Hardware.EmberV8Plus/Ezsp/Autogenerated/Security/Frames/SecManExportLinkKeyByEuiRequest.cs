using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Export the link key associated with the given EUI from the key table.
/// Frame value: 0x010D
/// </summary>
public class SecManExportLinkKeyByEuiRequest : EzspFrameRequest
{
    /// <summary>
    /// EUI64 associated with the key to export.
    /// </summary>
    public sl_802154_long_addr_t eui { get; set; }

