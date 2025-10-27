using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// A callback invoked when a network status/route error message is received. The error indicates that there was a problem sending/receiving messages from the target node
/// Frame value: 0x00C4
/// </summary>
public class IncomingNetworkStatusHandlerRequest : EzspFrameRequest
{
}
