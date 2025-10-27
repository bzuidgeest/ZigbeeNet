using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate the state of permit joining in MAC.
/// Frame value: 0x011F
/// </summary>
public class GetPermitJoiningResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether the current network permits joining.
    /// </summary>
    public bool joiningPermitted { get; set; }

}
