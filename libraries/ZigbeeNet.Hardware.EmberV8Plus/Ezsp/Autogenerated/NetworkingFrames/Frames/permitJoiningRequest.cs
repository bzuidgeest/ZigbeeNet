using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Tells the stack to allow other nodes to join the network with this node as their parent. Joining is initially disabled by default.
/// Frame value: 0x0022
/// </summary>
public class permitJoining : EzspFrameRequest
{
    /// <summary>
    /// A value of 0x00 disables joining. A value of 0xFF enables joining. Any other value enables joining for that number of seconds.
    /// </summary>
    public byte duration { get; set; }

}
