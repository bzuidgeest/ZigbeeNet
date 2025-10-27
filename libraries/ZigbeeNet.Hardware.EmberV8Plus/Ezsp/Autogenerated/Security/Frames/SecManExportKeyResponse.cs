using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Exports a key from security manager based on passed context.
/// Frame value: 0x0114
/// </summary>
public class SecManExportKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure code of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Data to store the exported key in.
    /// </summary>
    public sl_zigbee_sec_man_key_t key { get; set; }

}
