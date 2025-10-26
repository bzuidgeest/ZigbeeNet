using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the source route table total size.
/// Frame value: 0x00C3
/// </summary>
public class getSourceRouteTableTotalSizeResponse : EzspFrameResponse
{
    /// <summary>
    /// Total size of source route table.
    /// </summary>
    public byte sourceRouteTableTotalSize { get; set; }

}
