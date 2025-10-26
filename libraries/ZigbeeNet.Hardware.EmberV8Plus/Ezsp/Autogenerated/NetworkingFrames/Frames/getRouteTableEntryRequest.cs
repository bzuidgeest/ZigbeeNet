using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns the route table entry at the given index. The route table size can be obtained using the getConfigurationValue command.
/// Frame value: 0x007B
/// </summary>
public class getRouteTableEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the route table entry of interest.
    /// </summary>
    public byte index { get; set; }

}
