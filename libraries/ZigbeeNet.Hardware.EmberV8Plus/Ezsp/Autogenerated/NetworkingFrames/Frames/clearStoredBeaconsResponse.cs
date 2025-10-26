using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Clears all cached beacons that have been collected from a scan.
/// Frame value: 0x003C
/// </summary>
public class clearStoredBeaconsResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
