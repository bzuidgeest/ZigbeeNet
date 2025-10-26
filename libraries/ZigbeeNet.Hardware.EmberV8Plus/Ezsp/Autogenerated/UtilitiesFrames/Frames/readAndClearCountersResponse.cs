using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Retrieves and clears Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
/// Frame value: 0x0065
/// </summary>
public class readAndClearCountersResponse : EzspFrameResponse
{
    /// <summary>
    /// A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.
    /// </summary>
    public uint16_t[SL_ZIGBEE_COUNTER_TYPE_COUNT] values { get; set; }

}
