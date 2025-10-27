using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Indicates whether any messages are currently being sent using this address table entry. Note that this function does not indicate whether the address table entry is unused. To determine whether an address table entry is unused, check the remote node ID. The remote node ID will have the value SL_ZIGBEE_TABLE_ENTRY_UNUSED_NODE_ID when the address table entry is not in use.
/// Frame value: 0x005B
/// </summary>
public class AddressTableEntryIsActiveRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of an address table entry.
    /// </summary>
    public byte addressTableIndex { get; set; }

}
