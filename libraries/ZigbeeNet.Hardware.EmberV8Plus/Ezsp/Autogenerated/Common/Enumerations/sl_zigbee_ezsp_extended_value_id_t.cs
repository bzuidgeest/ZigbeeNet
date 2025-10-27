namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Identifies a value based on specified characteristics. Each set of characteristics is unique to that value and is specified during the call to get the extended value.
/// </summary>
public enum ZigbeeEzspExtendedValueId : byte
{
    /// <summary>
    /// The flags field associated with the specified endpoint.
    /// </summary>
    SL_ZIGBEE_EZSP_EXTENDED_VALUE_ENDPOINT_FLAGS = 0x00,
    /// <summary>
    /// This is the reason for the node to leave the network as well as the device that told it to leave. The leave reason is the 1st byte of the value while the node ID is the 2nd and 3rd byte. If the leave was caused due to an API call rather than an over the air message, the node ID will be SL_ZIGBEE_UNKNOWN_NODE_ID (0xFFFD).
    /// </summary>
    SL_ZIGBEE_EZSP_EXTENDED_VALUE_LAST_LEAVE_REASON = 0x01,
    /// <summary>
    /// This number of bytes of overhead required in the network frame for source routing to a particular destination.
    /// </summary>
    SL_ZIGBEE_EZSP_EXTENDED_VALUE_GET_SOURCE_ROUTE_OVERHEAD = 0x02,
    /// <summary>
    /// These values are current or boot-time metrics gathered by the memory manager/buffer manager.
    /// </summary>
    SL_ZIGBEE_EZSP_EXTENDED_VALUE_MEMORY_USAGE_DATA = 0x03
}
