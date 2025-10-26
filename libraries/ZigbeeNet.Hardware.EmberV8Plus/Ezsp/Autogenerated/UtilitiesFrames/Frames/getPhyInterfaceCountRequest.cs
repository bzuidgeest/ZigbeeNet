using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Returns number of phy interfaces present.
/// Frame value: 0x00FC
/// </summary>
public class getPhyInterfaceCount : EzspFrameRequest
{
}
