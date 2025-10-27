using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// Detects if the standalone bootloader is installed, and if so returns the installed version. If not return 0xffff. A returned version of 0x1234 would indicate version 1.2 build 34. Also return the node&apos;s version of PLAT, MICRO and PHY.
/// Frame value: 0x0091
/// </summary>
public class GetStandaloneBootloaderVersionPlatMicroPhyResponse : EzspFrameResponse
{
    /// <summary>
    /// BOOTLOADER_INVALID_VERSION if the standalone bootloader is not present, or the version of the installed standalone bootloader.
    /// </summary>
    public ushort bootloader_version { get; set; }

    /// <summary>
    /// The value of PLAT on the node
    /// </summary>
    public byte nodePlat { get; set; }

    /// <summary>
    /// The value of MICRO on the node
    /// </summary>
    public byte nodeMicro { get; set; }

    /// <summary>
    /// The value of PHY on the node
    /// </summary>
    public byte nodePhy { get; set; }

}
