using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This function starts a series of scans which will return an available panId.
/// Frame value: 0x00D3
/// </summary>
public class FindUnusedPanIdResponse : EzspFrameResponse
{
    /// <summary>
    /// The error condition that occurred during the scan. Value will be SL_STATUS_OK if there are no errors.
    /// </summary>
    public sl_status_t status { get; set; }

}
