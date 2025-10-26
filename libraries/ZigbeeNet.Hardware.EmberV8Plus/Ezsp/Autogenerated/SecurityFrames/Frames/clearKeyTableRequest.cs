using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// This function clears the key table of the current network.
/// Frame value: 0x00B1
/// </summary>
public class clearKeyTable : EzspFrameRequest
{
}
