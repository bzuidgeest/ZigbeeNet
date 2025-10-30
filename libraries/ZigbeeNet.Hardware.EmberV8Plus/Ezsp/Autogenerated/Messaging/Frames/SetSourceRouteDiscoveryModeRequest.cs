using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sets source route discovery(MTORR) mode to on, off, reschedule
/// Frame value: 0x005A
/// </summary>
public class SetSourceRouteDiscoveryModeRequest : EzspFrameRequest
{
    /// <summary>
    /// Source route discovery mode: off:0, on:1, reschedule:2
    /// </summary>
    public byte mode { get; set; }

