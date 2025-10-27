using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Imports a key into security manager based on passed context.
/// Frame value: 0x0115
/// </summary>
public class SecManImportKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure code of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
