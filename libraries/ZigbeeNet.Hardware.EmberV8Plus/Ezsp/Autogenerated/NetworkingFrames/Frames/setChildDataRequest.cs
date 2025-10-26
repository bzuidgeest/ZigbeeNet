using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets child data to the child table token.
/// Frame value: 0x00AC
/// </summary>
public class setChildData : EzspFrameRequest
{
    /// <summary>
    /// The index of the child of interest in the child table. Possible indexes range from zero to (SL_ZIGBEE_CHILD_TABLE_SIZE - 1).
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// The data of the child.
    /// </summary>
    public sl_zigbee_child_data_t childData { get; set; }

}
