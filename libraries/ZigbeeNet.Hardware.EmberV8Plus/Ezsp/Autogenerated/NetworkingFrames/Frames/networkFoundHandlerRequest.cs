using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Reports that a network was found as a result of a prior call to startScan. Gives the network parameters useful for deciding which network to join.
/// Frame value: 0x001B
/// </summary>
public class networkFoundHandler : EzspFrameRequest
{
}
