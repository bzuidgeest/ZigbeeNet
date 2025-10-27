using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the status of the current scan of type SL_ZIGBEE_EZSP_ENERGY_SCAN or SL_ZIGBEE_EZSP_ACTIVE_SCAN. SL_STATUS_OK signals that the scan has completed. Other error conditions signify a failure to scan on the channel specified.
/// Frame value: 0x001C
/// </summary>
public class ScanCompleteHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The channel on which the current error occurred. Undefined for the case of SL_STATUS_OK.
    /// </summary>
    public byte channel { get; set; }

    /// <summary>
    /// The error condition that occurred on the current channel. Value will be SL_STATUS_OK when the scan has completed.
    /// </summary>
    public sl_status_t status { get; set; }

}
