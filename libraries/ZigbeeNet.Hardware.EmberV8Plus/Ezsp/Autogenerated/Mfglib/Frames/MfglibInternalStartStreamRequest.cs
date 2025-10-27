using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Starts transmitting a random stream of characters. This is so that the radio modulation can be measured.
/// Frame value: 0x0087
/// </summary>
public class MfglibInternalStartStreamRequest : EzspFrameRequest
{
}
