using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Finds the index of the passed address in the gp table.
/// Frame value: 0x00DE
/// </summary>
public class GpSinkTableLookupRequest : EzspFrameRequest
{
    /// <summary>
    /// The address to search for.
    /// </summary>
    public sl_zigbee_gp_address_t addr { get; set; }

}
