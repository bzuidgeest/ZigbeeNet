using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Get the current multiprotocol sliptime
/// Frame value: 0x012C
/// </summary>
public class RadioGetSchedulerSliptimeResponse : EzspFrameResponse
{
    /// <summary>
    /// Value of the current slip time.
    /// </summary>
    public uint32_t[1] slipTime { get; set; }

}
