using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function clears the key table of the current network.
/// Frame value: 0x00B1
/// </summary>
public class ClearKeyTableResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
