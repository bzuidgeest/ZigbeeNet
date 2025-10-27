using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns information about a child of the local node.
/// Frame value: 0x004A
/// </summary>
public class GetChildDataRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.
    /// </summary>
    public byte index { get; set; }

}
