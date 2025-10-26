using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Clears all cached beacons that have been collected from a scan.
/// Frame value: 0x003C
/// </summary>
public class clearStoredBeacons : EzspFrameRequest
{
}
