using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Export the link key at given index from the key table.
/// Frame value: 0x010F
/// </summary>
public class SecManExportLinkKeyByIndexRequest : EzspFrameRequest
{
    /// <summary>
    /// Index of key to export.
    /// </summary>
    public byte index { get; set; }

}
