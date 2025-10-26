using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Returns the current radio channel, as previously set via mfglibInternalSetChannel().
/// Frame value: 0x008b
/// </summary>
public class mfglibInternalGetChannel : EzspFrameRequest
{
}
