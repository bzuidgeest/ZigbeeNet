using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Get the current multiprotocol sliptime
/// Frame value: 0x012C
/// </summary>
public class radioGetSchedulerSliptimeResponse : EzspFrameResponse
{
    /// <summary>
    /// Value of the current slip time.
    /// </summary>
    public uint32_t[1] slipTime { get; set; }

}
