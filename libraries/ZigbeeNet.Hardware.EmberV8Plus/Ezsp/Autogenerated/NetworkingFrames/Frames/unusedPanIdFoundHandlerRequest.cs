using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// This function returns an unused panID and channel pair found via the find unused panId scan procedure.
/// Frame value: 0x00D2
/// </summary>
public class unusedPanIdFoundHandler : EzspFrameRequest
{
}
