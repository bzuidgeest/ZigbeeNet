using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// A callback from the timer.
/// Frame value: 0x000F
/// </summary>
public class TimerHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Which timer generated the callback (0 or 1).
    /// </summary>
    public byte timerId { get; set; }

}
