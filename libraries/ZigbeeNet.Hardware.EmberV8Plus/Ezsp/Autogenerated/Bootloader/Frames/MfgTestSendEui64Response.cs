using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 8-byte EUI ID. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new EUI ID within 150 milliseconds.
/// Frame value: 0x014A
/// </summary>
public class MfgTestSendEui64Response : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
