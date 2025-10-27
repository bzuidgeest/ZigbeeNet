using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// A callback to the GP endpoint to indicate the result of the GPDF transmission.
/// Frame value: 0x00C7
/// </summary>
public class DGpSentHandlerRequest : EzspFrameRequest
{
}
