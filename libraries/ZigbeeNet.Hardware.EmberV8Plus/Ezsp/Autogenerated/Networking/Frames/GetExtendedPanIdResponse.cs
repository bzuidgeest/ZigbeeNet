using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Get the 8-byte extended PAN ID of this node.
/// Frame value: 0x0127
/// </summary>
public class GetExtendedPanIdResponse : EzspFrameResponse
{
    /// <summary>
    /// Extended PAN ID of this node.  Valid only if it is currently on a network.
    /// </summary>
    public uint8_t[8] extendedPanId { get; set; }

}
