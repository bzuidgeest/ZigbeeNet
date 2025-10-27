using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Returns number of phy interfaces present.
/// Frame value: 0x00FC
/// </summary>
public class GetPhyInterfaceCountResponse : EzspFrameResponse
{
    /// <summary>
    /// Value indicate how many phy interfaces present.
    /// </summary>
    public byte interfaceCount { get; set; }

}
