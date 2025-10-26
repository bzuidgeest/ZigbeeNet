using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Finds the index of the passed address in the gp table.
/// Frame value: 0x00DE
/// </summary>
public class gpSinkTableLookup : EzspFrameRequest
{
    /// <summary>
    /// The address to search for.
    /// </summary>
    public sl_zigbee_gp_address_t addr { get; set; }

}
