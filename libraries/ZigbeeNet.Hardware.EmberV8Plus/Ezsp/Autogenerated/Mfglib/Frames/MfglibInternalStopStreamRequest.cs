using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Stops transmitting a random stream of characters started by mfglibInternalStartStream().
/// Frame value: 0x0088
/// </summary>
public class MfglibInternalStopStreamRequest : EzspFrameRequest
{
}
