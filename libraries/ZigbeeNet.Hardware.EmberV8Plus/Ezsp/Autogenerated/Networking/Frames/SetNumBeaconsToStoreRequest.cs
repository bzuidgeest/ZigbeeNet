using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Configure the number of beacons to store when issuing active scans for networks.
/// Frame value: 0x0037
/// </summary>
public class SetNumBeaconsToStoreRequest : EzspFrameRequest
{
    /// <summary>
    /// The number of beacons to cache when scanning.
    /// </summary>
    public byte numBeacons { get; set; }

}
