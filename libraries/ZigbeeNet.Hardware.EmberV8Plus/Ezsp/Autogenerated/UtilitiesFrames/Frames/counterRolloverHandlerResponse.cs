using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// This call is fired when a counter exceeds its threshold
/// Frame value: 0x00F2
/// </summary>
public class counterRolloverHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Type of Counter
    /// </summary>
    public sl_zigbee_counter_type_t type { get; set; }

}
