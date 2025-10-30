using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked when a route error message is received. The error indicates that a problem routing to or from the target node was encountered.
/// Frame value: 0x0080
/// </summary>
public class IncomingRouteErrorHandlerRequest : EzspFrameRequest
{
