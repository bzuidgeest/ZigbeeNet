using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when a ZLL network scan finds a ZLL network.
/// Frame value: 0x00B6
/// </summary>
public class ZllNetworkFoundHandlerRequest : EzspFrameRequest
{
