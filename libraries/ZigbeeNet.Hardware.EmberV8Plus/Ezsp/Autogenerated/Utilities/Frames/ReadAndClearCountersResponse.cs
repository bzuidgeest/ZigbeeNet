using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Retrieves and clears Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
/// Frame value: 0x0065
/// </summary>
public class ReadAndClearCountersResponse : EzspFrameResponse
{
    /// <summary>
    /// A list of all counter values ordered according to the sl_zigbee_counter_type_t enumeration.
    /// </summary>
    public uint16_t[SL_ZIGBEE_COUNTER_TYPE_COUNT] values { get; set; }

}
