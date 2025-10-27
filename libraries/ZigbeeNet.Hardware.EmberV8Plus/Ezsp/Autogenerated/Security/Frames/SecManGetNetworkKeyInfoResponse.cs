using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Retrieve information about the current and alternate network key, excluding their contents.
/// Frame value: 0x0116
/// </summary>
public class SecManGetNetworkKeyInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// Success or failure of retrieving network key info.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Information about current and alternate network keys.
    /// </summary>
    public sl_zigbee_sec_man_network_key_info_t network_key_info { get; set; }

}
