using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
/// Frame value: 0x008f
/// </summary>
public class LaunchStandaloneBootloaderResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
