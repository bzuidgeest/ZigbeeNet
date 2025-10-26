using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns information about a source route table entry
/// Frame value: 0x00C1
/// </summary>
public class getSourceRouteTableEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the entry of interest in the
source route table. Possible indexes range from zero to
SOURCE_ROUTE_TABLE_FILLED_SIZE.
    /// </summary>
    public byte index { get; set; }

}
