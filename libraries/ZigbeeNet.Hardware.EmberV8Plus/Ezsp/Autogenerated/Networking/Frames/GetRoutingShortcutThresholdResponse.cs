using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Gets the routing shortcut threshold used to differentiate between directly using a neighbor vs. performing routing.
/// Frame value: 0x00D1
/// </summary>
public class GetRoutingShortcutThresholdResponse : EzspFrameResponse
{
    /// <summary>
    /// The routing shortcut threshold
    /// </summary>
    public byte routingShortcutThresh { get; set; }

}
