using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Convert a child index to a node ID
/// Frame value: 0x0106
/// </summary>
public class childId : EzspFrameRequest
{
    /// <summary>
    /// The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.
    /// </summary>
    public byte childIndex { get; set; }

}
