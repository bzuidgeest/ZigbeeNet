using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Returns the current radio power setting, as previously set via mfglibInternalSetPower().
/// Frame value: 0x008d
/// </summary>
public class mfglibInternalGetPower : EzspFrameRequest
{
}
