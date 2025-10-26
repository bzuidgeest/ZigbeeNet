using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Sets the EUI64 and short ID of an address table entry. Usually the application will not need to set the short ID in the address table. Once the remote EUI64 is set the stack is capable of figuring out the short ID on its own. However, in cases where the application does set the short ID, the application must set the remote EUI64 prior to setting the short ID. This function will also check other address table entries, the child table and the neighbor table to see if the node ID for the given EUI64 is already known. If known then this function will set node ID. If not known it will set the node ID to SL_ZIGBEE_UNKNOWN_NODE_ID.
/// Frame value: 0x005C
/// </summary>
public class setAddressTableInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the information was successfully set, and SL_STATUS_ZIGBEE_ADDRESS_TABLE_ENTRY_IS_ACTIVE otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

}
