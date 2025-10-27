using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Convert a child index to a node ID
/// Frame value: 0x0106
/// </summary>
public class ChildIdRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.
    /// </summary>
    public byte childIndex { get; set; }

}
