using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Returns the current radio channel, as previously set via mfglibInternalSetChannel().
/// Frame value: 0x008b
/// </summary>
public class MfglibInternalGetChannelRequest : EzspFrameRequest
{
