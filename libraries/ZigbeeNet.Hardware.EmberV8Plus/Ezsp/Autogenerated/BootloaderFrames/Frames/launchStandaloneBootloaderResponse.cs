using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
/// Frame value: 0x008f
/// </summary>
public class launchStandaloneBootloaderResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
