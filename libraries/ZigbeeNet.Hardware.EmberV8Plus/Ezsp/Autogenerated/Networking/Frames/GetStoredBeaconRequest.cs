using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Fetches the specified beacon in the cache. Beacons are stored in cache after issuing an active scan.
/// Frame value: 0x0004
/// </summary>
public class GetStoredBeaconRequest : EzspFrameRequest
{
    /// <summary>
    /// The beacon index to fetch. Valid values range from 0 to &lt;i&gt;sli_zigbee_stack_get_num_stored_beacons&lt;/i&gt;-1.
    /// </summary>
    public byte beacon_number { get; set; }

}
