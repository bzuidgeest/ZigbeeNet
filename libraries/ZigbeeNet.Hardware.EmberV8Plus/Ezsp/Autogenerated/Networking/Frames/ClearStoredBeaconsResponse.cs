using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Clears all cached beacons that have been collected from a scan.
/// Frame value: 0x003C
/// </summary>
public class ClearStoredBeaconsResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
