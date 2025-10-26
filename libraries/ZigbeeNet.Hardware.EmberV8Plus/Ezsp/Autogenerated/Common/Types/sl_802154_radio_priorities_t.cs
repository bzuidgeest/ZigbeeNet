namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Scheduler priorities for radio operations
/// </summary>
public struct sl_802154_radio_priorities_t
{
    /// <summary>
    /// The priority of a Zigbee RX operation while not receiving a packet
    /// </summary>
    public byte background_rx;

    /// <summary>
    /// Starting priority of a Zigbee TX operation. The first transmit of the packet, before retries, uses this priority
    /// </summary>
    public byte min_tx_priority;

    /// <summary>
    /// The increase in TX priority (which is a decrement in value) for each retry
    /// </summary>
    public byte tx_step;

    /// <summary>
    /// Maximum priority of a Zigbee TX operation. Retried messages have priorities bumped by tx_step, up to a maximum of max_tx_priority
    /// </summary>
    public byte max_tx_priority;

    /// <summary>
    /// The priority of a Zigbee RX operation while receiving a packet
    /// </summary>
    public byte active_rx;

}

