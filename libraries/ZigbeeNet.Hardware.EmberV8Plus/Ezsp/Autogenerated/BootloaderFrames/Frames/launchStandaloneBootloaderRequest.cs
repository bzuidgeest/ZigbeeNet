using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Command;

/// <summary>
/// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
/// Frame value: 0x008f
/// </summary>
public class launchStandaloneBootloader : EzspFrameRequest
{
    /// <summary>
    /// If true, launch the standalone bootloader. If false, do nothing.
    /// </summary>
    public bool enabled { get; set; }

}
