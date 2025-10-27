using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the number of cached beacons that have been collected from a scan.
/// Frame value: 0x0008
/// </summary>
public class GetNumStoredBeaconsRequest : EzspFrameRequest
{
}
