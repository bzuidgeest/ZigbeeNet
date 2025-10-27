using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Import a transient link key.
/// Frame value: 0x0111
/// </summary>
public class SecManImportTransientKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key import operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
