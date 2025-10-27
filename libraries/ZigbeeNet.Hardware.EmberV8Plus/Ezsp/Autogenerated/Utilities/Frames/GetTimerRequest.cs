using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Gets information about a timer. The Host can use this command to find out how much longer it will be before a previously set timer will generate a callback.
/// Frame value: 0x004E
/// </summary>
public class GetTimerRequest : EzspFrameRequest
{
    /// <summary>
    /// Which timer to get information about (0 or 1).
    /// </summary>
    public byte timerId { get; set; }

}
