using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked by the EmberZNet stack when an id conflict is discovered, that is, two different nodes in the network were found to be using the same short id. The stack automatically removes the conflicting short id from its internal tables (address, binding, route, neighbor, and child tables). The application should discontinue any other use of the id.
/// Frame value: 0x007C
/// </summary>
public class IdConflictHandlerRequest : EzspFrameRequest
{
}
