using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Starts periodic many-to-one route discovery. Periodic discovery is started by default on bootup, but this function may be used if discovery has been stopped by a call to ::emberConcentratorStopDiscovery().
/// Frame value: 0x014F
/// </summary>
public class ConcentratorStartDiscoveryResponse : EzspFrameResponse
{
}
