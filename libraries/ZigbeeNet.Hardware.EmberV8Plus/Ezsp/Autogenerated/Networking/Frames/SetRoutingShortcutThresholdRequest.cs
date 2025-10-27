using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sets the routing shortcut threshold to directly use a neighbor instead of performing routing.
/// Frame value: 0x00D0
/// </summary>
public class SetRoutingShortcutThresholdRequest : EzspFrameRequest
{
    /// <summary>
    /// The routing shortcut threshold to configure.
    /// </summary>
    public byte costThresh { get; set; }

}
