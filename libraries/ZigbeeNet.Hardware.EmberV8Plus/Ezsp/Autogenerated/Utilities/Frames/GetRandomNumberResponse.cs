using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Returns a pseudorandom number.
/// Frame value: 0x0049
/// </summary>
public class GetRandomNumberResponse : EzspFrameResponse
{
    /// <summary>
    /// Always returns SL_STATUS_OK.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// A pseudorandom number.
    /// </summary>
    public ushort value { get; set; }

}
