using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// Detects if the standalone bootloader is installed, and if so returns the installed version. If not return 0xffff. A returned version of 0x1234 would indicate version 1.2 build 34. Also return the node&apos;s version of PLAT, MICRO and PHY.
/// Frame value: 0x0091
/// </summary>
public class GetStandaloneBootloaderVersionPlatMicroPhyRequest : EzspFrameRequest
{
}
