using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Exports a key from security manager based on passed context.
/// Frame value: 0x0114
/// </summary>
public class SecManExportKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// Metadata to identify the requested key.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

