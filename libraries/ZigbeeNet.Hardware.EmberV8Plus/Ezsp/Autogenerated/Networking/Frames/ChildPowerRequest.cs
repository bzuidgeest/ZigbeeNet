using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Return radio power value of the child from the given childIndex
/// Frame value: 0x0134
/// </summary>
public class ChildPowerRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the child of interest in the child table. Possible indexes range from zero to SL_ZIGBEE_CHILD_TABLE_SIZE.
    /// </summary>
    public byte childIndex { get; set; }

