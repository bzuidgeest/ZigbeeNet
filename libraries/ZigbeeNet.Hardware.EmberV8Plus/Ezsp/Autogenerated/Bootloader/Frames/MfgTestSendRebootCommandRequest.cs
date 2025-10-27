using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to send the DUT a reboot command. The usual practice is to execute this command at the end of manufacturing configuration, to place the DUT into normal network operation for testing. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the reboot command within 20 milliseconds and then reboots.
/// Frame value: 0x0149
/// </summary>
public class MfgTestSendRebootCommandRequest : EzspFrameRequest
{
}
