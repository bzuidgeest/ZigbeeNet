using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Retrieves Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
/// Frame value: 0x00F1
/// </summary>
public class readCounters : EzspFrameRequest
{
}
