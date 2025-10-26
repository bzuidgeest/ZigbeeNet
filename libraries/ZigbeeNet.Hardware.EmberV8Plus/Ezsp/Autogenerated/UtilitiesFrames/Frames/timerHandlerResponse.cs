using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// A callback from the timer.
/// Frame value: 0x000F
/// </summary>
public class timerHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Which timer generated the callback (0 or 1).
    /// </summary>
    public byte timerId { get; set; }

}
