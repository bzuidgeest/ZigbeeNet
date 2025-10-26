using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// A callback invoked when a route error message is received. The error indicates that a problem routing to or from the target node was encountered.
/// Frame value: 0x0080
/// </summary>
public class incomingRouteErrorHandler : EzspFrameRequest
{
}
