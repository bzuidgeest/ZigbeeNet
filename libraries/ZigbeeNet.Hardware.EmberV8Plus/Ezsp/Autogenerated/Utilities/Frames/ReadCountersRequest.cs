using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Retrieves Ember counters. See the sl_zigbee_counter_type_t enumeration for the counter types.
/// Frame value: 0x00F1
/// </summary>
public class ReadCountersRequest : EzspFrameRequest
{
