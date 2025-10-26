using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Get the current network.
/// Frame value: 0x014E
/// </summary>
public class getCurrentNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// Return the current network index.
    /// </summary>
    public byte index { get; set; }

}
