using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Stops periodic many-to-one route discovery.
/// Frame value: 0x0150
/// </summary>
public class ConcentratorStopDiscoveryRequest : EzspFrameRequest
{
}
