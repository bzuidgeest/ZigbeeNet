using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback indicating that a many-to-one route to the concentrator with the given short and long id is available for use.
/// Frame value: 0x007D
/// </summary>
public class IncomingManyToOneRouteRequestHandlerRequest : EzspFrameRequest
{
}
