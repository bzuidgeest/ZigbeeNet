using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the source route table total size.
/// Frame value: 0x00C3
/// </summary>
public class GetSourceRouteTableTotalSizeResponse : EzspFrameResponse
{
    /// <summary>
    /// Total size of source route table.
    /// </summary>
    public byte sourceRouteTableTotalSize { get; set; }

}
