using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used in each of the manufacturing configuration API calls. Most implementations will not need to call this function directly. See mfg-test.c for more detail. This function executes only during manufacturing configuration mode and returns an error otherwise.
/// Frame value: 0x014D
/// </summary>
public class MfgTestSendCommandResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
