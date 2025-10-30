using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Check if a particular counter can report on the destination node ID they have been triggered from.
/// Frame value: 0x0133
/// </summary>
public class CounterRequiresDestinationNodeIdRequest : EzspFrameRequest
{
    /// <summary>
    /// The counter to be checked.
    /// </summary>
    public sl_zigbee_counter_type_t counter { get; set; }

