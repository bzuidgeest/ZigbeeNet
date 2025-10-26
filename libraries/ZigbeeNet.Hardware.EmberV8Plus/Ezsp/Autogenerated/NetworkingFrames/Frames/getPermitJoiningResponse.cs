using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Indicate the state of permit joining in MAC.
/// Frame value: 0x011F
/// </summary>
public class getPermitJoiningResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether the current network permits joining.
    /// </summary>
    public bool joiningPermitted { get; set; }

}
