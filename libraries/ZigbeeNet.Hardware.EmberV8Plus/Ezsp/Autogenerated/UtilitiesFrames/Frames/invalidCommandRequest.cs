using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Indicates that the NCP received an invalid command.
/// Frame value: 0x0058
/// </summary>
public class invalidCommand : EzspFrameRequest
{
}
