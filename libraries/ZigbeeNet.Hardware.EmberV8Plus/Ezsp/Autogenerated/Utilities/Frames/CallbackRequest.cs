using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Allows the NCP to respond with a pending callback.
/// Frame value: 0x0006
/// </summary>
public class CallbackRequest : EzspFrameRequest
{
