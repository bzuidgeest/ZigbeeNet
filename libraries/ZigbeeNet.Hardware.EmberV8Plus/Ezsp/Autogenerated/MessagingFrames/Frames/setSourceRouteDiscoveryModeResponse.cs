using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Sets source route discovery(MTORR) mode to on, off, reschedule
/// Frame value: 0x005A
/// </summary>
public class setSourceRouteDiscoveryModeResponse : EzspFrameResponse
{
    /// <summary>
    /// Remaining time(ms) until next MTORR broadcast if the mode is on, MAX_INT32U_VALUE if the mode is off
    /// </summary>
    public uint remainingTime { get; set; }

}
