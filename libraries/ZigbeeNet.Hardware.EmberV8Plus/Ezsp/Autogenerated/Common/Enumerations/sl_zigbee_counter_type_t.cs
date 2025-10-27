namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Defines the events reported to the application by the &lt;i&gt;readAndClearCounters&lt;/i&gt; command.
/// </summary>
public enum ZigbeeCounterType : byte
{
    /// <summary>
    /// The MAC received a broadcast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_RX_BROADCAST = 0,
    /// <summary>
    /// The MAC transmitted a broadcast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_TX_BROADCAST = 1,
    /// <summary>
    /// The MAC received a unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_RX_UNICAST = 2,
    /// <summary>
    /// The MAC successfully transmitted a unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_TX_UNICAST_SUCCESS = 3,
    /// <summary>
    /// The MAC retried a unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_TX_UNICAST_RETRY = 4,
    /// <summary>
    /// The MAC unsuccessfully transmitted a unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_MAC_TX_UNICAST_FAILED = 5,
    /// <summary>
    /// The APS layer received a data broadcast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_RX_BROADCAST = 6,
    /// <summary>
    /// The APS layer transmitted a data broadcast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_TX_BROADCAST = 7,
    /// <summary>
    /// The APS layer received a data unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_RX_UNICAST = 8,
    /// <summary>
    /// The APS layer successfully transmitted a data unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_TX_UNICAST_SUCCESS = 9,
    /// <summary>
    /// The APS layer retried a data unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_TX_UNICAST_RETRY = 10,
    /// <summary>
    /// The APS layer unsuccessfully transmitted a data unicast.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DATA_TX_UNICAST_FAILED = 11,
    /// <summary>
    /// The network layer successfully submitted a new route discovery to the MAC.
    /// </summary>
    SL_ZIGBEE_COUNTER_ROUTE_DISCOVERY_INITIATED = 12,
    /// <summary>
    /// An entry was added to the neighbor table.
    /// </summary>
    SL_ZIGBEE_COUNTER_NEIGHBOR_ADDED = 13,
    /// <summary>
    /// An entry was removed from the neighbor table.
    /// </summary>
    SL_ZIGBEE_COUNTER_NEIGHBOR_REMOVED = 14,
    /// <summary>
    /// A neighbor table entry became stale because it had not been heard from.
    /// </summary>
    SL_ZIGBEE_COUNTER_NEIGHBOR_STALE = 15,
    /// <summary>
    /// A node joined or rejoined to the network via this node.
    /// </summary>
    SL_ZIGBEE_COUNTER_JOIN_INDICATION = 16,
    /// <summary>
    /// An entry was removed from the child table.
    /// </summary>
    SL_ZIGBEE_COUNTER_CHILD_REMOVED = 17,
    /// <summary>
    /// EZSP-UART only. An overflow error occurred in the UART.
    /// </summary>
    SL_ZIGBEE_COUNTER_ASH_OVERFLOW_ERROR = 18,
    /// <summary>
    /// EZSP-UART only. A framing error occurred in the UART.
    /// </summary>
    SL_ZIGBEE_COUNTER_ASH_FRAMING_ERROR = 19,
    /// <summary>
    /// EZSP-UART only. An overrun error occurred in the UART.
    /// </summary>
    SL_ZIGBEE_COUNTER_ASH_OVERRUN_ERROR = 20,
    /// <summary>
    /// A message was dropped at the network layer because the NWK frame counter was not higher than the last message seen from that source.
    /// </summary>
    SL_ZIGBEE_COUNTER_NWK_FRAME_COUNTER_FAILURE = 21,
    /// <summary>
    /// A message was dropped at the APS layer because the APS frame counter was not higher than the last message seen from that source.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_FRAME_COUNTER_FAILURE = 22,
    /// <summary>
    /// Utility counter for general debugging use.
    /// </summary>
    EMBER_COUNTER_UTILITY = 23,
    /// <summary>
    /// A message was dropped at the APS layer because it had APS encryption but the key associated with the sender has not been authenticated, and thus the key is not authorized for use in APS data messages.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_LINK_KEY_NOT_AUTHORIZED = 24,
    /// <summary>
    /// An NWK encrypted message was received but dropped because decryption failed.
    /// </summary>
    SL_ZIGBEE_COUNTER_NWK_DECRYPTION_FAILURE = 25,
    /// <summary>
    /// An APS encrypted message was received but dropped because decryption failed.
    /// </summary>
    SL_ZIGBEE_COUNTER_APS_DECRYPTION_FAILURE = 26,
    /// <summary>
    /// The number of times we failed to allocate a set of linked packet buffers. This doesn&apos;t necessarily mean that the packet buffer count was 0 at the time, but that the number requested was greater than the number free.
    /// </summary>
    SL_ZIGBEE_COUNTER_ALLOCATE_PACKET_BUFFER_FAILURE = 27,
    /// <summary>
    /// The number of relayed unicast packets.
    /// </summary>
    SL_ZIGBEE_COUNTER_RELAYED_UNICAST = 28,
    /// <summary>
    /// The number of times we dropped a packet due to reaching the preset PHY to MAC queue limit (sli_802154mac_max_phy_to_mac_queue_length).
    /// </summary>
    SL_ZIGBEE_COUNTER_PHY_TO_MAC_QUEUE_LIMIT_REACHED = 29,
    /// <summary>
    /// The number of times we dropped a packet due to the packet-validate library checking a packet and rejecting it due to length or other formatting problems.
    /// </summary>
    SL_ZIGBEE_COUNTER_PACKET_VALIDATE_LIBRARY_DROPPED_COUNT = 30,
    /// <summary>
    /// The number of times the NWK retry queue is full and a new message failed to be added.
    /// </summary>
    SL_ZIGBEE_COUNTER_TYPE_NWK_RETRY_OVERFLOW = 31,
    /// <summary>
    /// The number of times the PHY layer was unable to transmit due to a failed CCA.
    /// </summary>
    SL_ZIGBEE_COUNTER_PHY_CCA_FAIL_COUNT = 32,
    /// <summary>
    /// The number of times an NWK broadcast was dropped because the broadcast table was full.
    /// </summary>
    SL_ZIGBEE_COUNTER_BROADCAST_TABLE_FULL = 33,
    /// <summary>
    /// The number of low priority packet traffic arbitration requests.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_LO_PRI_REQUESTED = 34,
    /// <summary>
    /// The number of high priority packet traffic arbitration requests.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_HI_PRI_REQUESTED = 35,
    /// <summary>
    /// The number of low priority packet traffic arbitration requests denied.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_LO_PRI_DENIED = 36,
    /// <summary>
    /// The number of high priority packet traffic arbitration requests denied.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_HI_PRI_DENIED = 37,
    /// <summary>
    /// The number of aborted low-priority packet traffic arbitration transmissions.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_LO_PRI_TX_ABORTED = 38,
    /// <summary>
    /// The number of aborted high-priority packet traffic arbitration transmissions.
    /// </summary>
    SL_ZIGBEE_COUNTER_PTA_HI_PRI_TX_ABORTED = 39,
    /// <summary>
    /// A placeholder giving the number of Ember counter types.
    /// </summary>
    SL_ZIGBEE_COUNTER_TYPE_COUNT = 40
}
