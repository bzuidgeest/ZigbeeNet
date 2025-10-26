using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Returns number of phy interfaces present.
/// Frame value: 0x00FC
/// </summary>
public class getPhyInterfaceCountResponse : EzspFrameResponse
{
    /// <summary>
    /// Value indicate how many phy interfaces present.
    /// </summary>
    public byte interfaceCount { get; set; }

}
