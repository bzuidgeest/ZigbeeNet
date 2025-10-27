using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Export a transient link key from a given table index.
/// Frame value: 0x0112
/// </summary>
public class SecManExportTransientKeyByIndexRequest : EzspFrameRequest
{
    /// <summary>
    /// Index to export from.
    /// </summary>
    public byte index { get; set; }

}
