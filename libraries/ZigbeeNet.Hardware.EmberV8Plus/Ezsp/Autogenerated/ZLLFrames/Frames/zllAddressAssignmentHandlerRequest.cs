using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// This call is fired when network and group addresses are assigned to a remote mode in a network start or network join request.
/// Frame value: 0x00B8
/// </summary>
public class zllAddressAssignmentHandler : EzspFrameRequest
{
}
