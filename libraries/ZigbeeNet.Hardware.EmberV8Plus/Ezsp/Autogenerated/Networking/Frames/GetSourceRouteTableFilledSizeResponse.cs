using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the number of filled entries in source route table.
/// Frame value: 0x00C2
/// </summary>
public class GetSourceRouteTableFilledSizeResponse : EzspFrameResponse
{
    /// <summary>
    /// The number of filled entries in source route table.
    /// </summary>
    public byte sourceRouteTableFilledSize { get; set; }

}
