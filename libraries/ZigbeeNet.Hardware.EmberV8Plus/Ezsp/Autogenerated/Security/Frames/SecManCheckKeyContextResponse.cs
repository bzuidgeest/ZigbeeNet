using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Check whether a key context can be used to load a valid key.
/// Frame value: 0x0110
/// </summary>
public class SecManCheckKeyContextResponse : EzspFrameResponse
{
    /// <summary>
    /// Validity of the checked context.
    /// </summary>
    public sl_status_t status { get; set; }

}
