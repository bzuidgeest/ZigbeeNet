using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used in each of the manufacturing configuration API calls. Most implementations will not need to call this function directly. See mfg-test.c for more detail. This function executes only during manufacturing configuration mode and returns an error otherwise.
/// Frame value: 0x014D
/// </summary>
public class MfgTestSendCommandRequest : EzspFrameRequest
{
    /// <summary>
    /// A pointer to the outgoing command string.
    /// </summary>
    public uint8_t[1] command { get; set; }

}
