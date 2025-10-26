using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Fetches the specified beacon in the cache. Beacons are stored in cache after issuing an active scan.
/// Frame value: 0x0004
/// </summary>
public class getStoredBeaconResponse : EzspFrameResponse
{
    /// <summary>
    /// An appropriate sl_status_t status code.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The beacon to populate upon success.
    /// </summary>
    public sl_zigbee_beacon_data_t beacon { get; set; }

}
