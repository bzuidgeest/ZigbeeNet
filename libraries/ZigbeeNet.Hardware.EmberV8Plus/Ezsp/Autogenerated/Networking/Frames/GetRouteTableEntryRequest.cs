using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the route table entry at the given index. The route table size can be obtained using the getConfigurationValue command.
/// Frame value: 0x007B
/// </summary>
public class GetRouteTableEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the route table entry of interest.
    /// </summary>
    public byte index { get; set; }

}
