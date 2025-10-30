using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// Quits the current application and launches the standalone bootloader (if installed) The function returns an error if the standalone bootloader is not present
/// Frame value: 0x008f
/// </summary>
public class LaunchStandaloneBootloaderRequest : EzspFrameRequest
{
    /// <summary>
    /// If true, launch the standalone bootloader. If false, do nothing.
    /// </summary>
    public bool enabled { get; set; }

