using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Finds or allocates a sink entry
/// Frame value: 0x00E1
/// </summary>
public class GpSinkTableFindOrAllocateEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// An sl_zigbee_gp_address_t struct containing a copy of the gpd address to be found.
    /// </summary>
    public sl_zigbee_gp_address_t addr { get; set; }

}
