using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s radio parameters. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new parameters within 25 milliseconds.
/// Frame value: 0x014C
/// </summary>
public class MfgTestSendRadioParametersResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
