namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeOutgoingMessageType : byte
{
    /// <summary>
    /// Unicast sent directly to an sl_802154_short_addr_t.
    /// </summary>
    SL_ZIGBEE_OUTGOING_DIRECT = 0x00,
    /// <summary>
    /// Unicast sent using an entry in the address table.
    /// </summary>
    SL_ZIGBEE_OUTGOING_VIA_ADDRESS_TABLE = 0x01,
    /// <summary>
    /// Unicast sent using an entry in the binding table.
    /// </summary>
    SL_ZIGBEE_OUTGOING_VIA_BINDING = 0x02,
    /// <summary>
    /// Multicast message. This value is passed to sli_zigbee_stack_message_sent_handler() only. It may not be passed to sli_zigbee_stack_send_unicast().
    /// </summary>
    SL_ZIGBEE_OUTGOING_MULTICAST = 0x03,
    /// <summary>
    /// Broadcast message. This value is passed to sli_zigbee_stack_message_sent_handler() only. It may not be passed to sli_zigbee_stack_send_unicast().
    /// </summary>
    SL_ZIGBEE_OUTGOING_BROADCAST = 0x04
}
