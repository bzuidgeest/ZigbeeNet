using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the number of cached beacons that have been collected from a scan.
/// Frame value: 0x0008
/// </summary>
public class getNumStoredBeaconsResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of cached beacons that have been collected from a scan.
    /// </summary>
    public byte numBeacons { get; set; }

}
