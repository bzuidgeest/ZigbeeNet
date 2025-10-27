using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Import an application link key into the key table.
/// Frame value: 0x010E
/// </summary>
public class SecManImportLinkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key import operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
