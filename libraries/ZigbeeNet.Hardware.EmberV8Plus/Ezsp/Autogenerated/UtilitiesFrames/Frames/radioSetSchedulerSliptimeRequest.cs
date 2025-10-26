using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Set the current multiprotocol sliptime
/// Frame value: 0x012D
/// </summary>
public class radioSetSchedulerSliptime : EzspFrameRequest
{
    /// <summary>
    /// Value of the current slip time.
    /// </summary>
    public uint slipTime { get; set; }

}
