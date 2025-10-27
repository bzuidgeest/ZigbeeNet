using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Get the current scheduler priorities for radio operations
/// Frame value: 0x012A
/// </summary>
public class RadioGetSchedulerPrioritiesRequest : EzspFrameRequest
{
}
