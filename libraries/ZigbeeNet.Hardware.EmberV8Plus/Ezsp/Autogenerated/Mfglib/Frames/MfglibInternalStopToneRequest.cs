using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Stops transmitting tone started by mfglibInternalStartTone().
/// Frame value: 0x0086
/// </summary>
public class MfglibInternalStopToneRequest : EzspFrameRequest
{
}
