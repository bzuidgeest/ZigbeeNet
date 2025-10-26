using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to send the DUT a reboot command. The usual practice is to execute this command at the end of manufacturing configuration, to place the DUT into normal network operation for testing. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the reboot command within 20 milliseconds and then reboots.
/// Frame value: 0x0149
/// </summary>
public class mfgTestSendRebootCommandResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
