using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Obtains the current duty cycle state.
/// Frame value: 0x0035
/// </summary>
public class GetDutyCycleStateRequest : EzspFrameRequest
{
