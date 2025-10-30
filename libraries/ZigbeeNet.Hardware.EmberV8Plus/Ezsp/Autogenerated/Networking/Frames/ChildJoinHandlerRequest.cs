using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicates that a child has joined or left.
/// Frame value: 0x0023
/// </summary>
public class ChildJoinHandlerRequest : EzspFrameRequest
{
