using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Stops periodic many-to-one route discovery.
/// Frame value: 0x0150
/// </summary>
public class concentratorStopDiscovery : EzspFrameRequest
{
}
