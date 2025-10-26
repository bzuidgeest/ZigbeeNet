using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Check if a particular counter can report on the destination node ID they have been triggered from.
/// Frame value: 0x0133
/// </summary>
public class counterRequiresDestinationNodeId : EzspFrameRequest
{
    /// <summary>
    /// The counter to be checked.
    /// </summary>
    public sl_zigbee_counter_type_t counter { get; set; }

}
