namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Beacon data structure.
/// </summary>
public struct sl_zigbee_beacon_data_t
{
    /// <summary>
    /// The channel of the received beacon.
    /// </summary>
    public byte channel;

    /// <summary>
    /// The LQI of the received beacon.
    /// </summary>
    public byte lqi;

    /// <summary>
    /// The RSSI of the received beacon.
    /// </summary>
    public sbyte rssi;

    /// <summary>
    /// The depth of the received beacon.
    /// </summary>
    public byte depth;

    /// <summary>
    /// The network update ID of the received beacon.
    /// </summary>
    public byte nwkUpdateId;

    /// <summary>
    /// The power level of the received beacon. This field is valid only if the beacon is an enhanced beacon.
    /// </summary>
    public sbyte power;

    /// <summary>
    /// The TC connectivity and long uptime from capacity field.
    /// </summary>
    public sbyte parentPriority;

    /// <summary>
    /// The PAN ID of the received beacon.
    /// </summary>
    public sl_802154_pan_id_t panId;

    /// <summary>
    /// The extended PAN ID of the received beacon.
    /// </summary>
    public fixed byte extendedPanId[8];

    /// <summary>
    /// The sender of the received beacon.
    /// </summary>
    public sl_802154_short_addr_t sender;

    /// <summary>
    /// Whether or not the beacon is enhanced.
    /// </summary>
    public bool enhanced;

    /// <summary>
    /// Whether the beacon is advertising permit join.
    /// </summary>
    public bool permitJoin;

    /// <summary>
    /// Whether the beacon is advertising capacity.
    /// </summary>
    public bool hasCapacity;

}

