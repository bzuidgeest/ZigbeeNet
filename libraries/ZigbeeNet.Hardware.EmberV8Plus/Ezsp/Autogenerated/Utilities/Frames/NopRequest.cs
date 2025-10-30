using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// A command which does nothing. The Host can use this to set the sleep mode or to check the status of the NCP.
/// Frame value: 0x0005
/// </summary>
public class NopRequest : EzspFrameRequest
{
