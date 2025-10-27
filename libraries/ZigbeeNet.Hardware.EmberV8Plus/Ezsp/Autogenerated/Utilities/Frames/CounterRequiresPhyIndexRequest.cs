using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Check if a particular counter is one that could report from either a 2.4GHz or sub-GHz interface.
/// Frame value: 0x0132
/// </summary>
public class CounterRequiresPhyIndexRequest : EzspFrameRequest
{
    /// <summary>
    /// The counter to be checked.
    /// </summary>
    public sl_zigbee_counter_type_t counter { get; set; }

}
