using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Configure the number of beacons to store when issuing active scans for networks.
/// Frame value: 0x0037
/// </summary>
public class setNumBeaconsToStore : EzspFrameRequest
{
    /// <summary>
    /// The number of beacons to cache when scanning.
    /// </summary>
    public byte numBeacons { get; set; }

}
