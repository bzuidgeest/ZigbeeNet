using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the route table entry at the given index. The route table size can be obtained using the getConfigurationValue command.
/// Frame value: 0x007B
/// </summary>
public class getRouteTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_FAIL if the index is out of range or the device is an end device, and SL_STATUS_OK otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The contents of the route table entry.
    /// </summary>
    public sl_zigbee_route_table_entry_t value { get; set; }

}
