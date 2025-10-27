using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Return the number of router children that the node currently has.
/// Frame value: 0x013B
/// </summary>
public class RouterChildCountResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of router children.
    /// </summary>
    public byte routerChildCount { get; set; }

}
