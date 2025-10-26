namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Information about a specific ZLL Device.
/// </summary>
public struct sl_zigbee_zll_device_info_record_t
{
    /// <summary>
    /// EUI64 associated with the device.
    /// </summary>
    public sl_802154_long_addr_t ieeeAddress;

    /// <summary>
    /// Endpoint id.
    /// </summary>
    public byte endpointId;

    /// <summary>
    /// Profile id.
    /// </summary>
    public ushort profileId;

    /// <summary>
    /// Device id.
    /// </summary>
    public ushort deviceId;

    /// <summary>
    /// Associated version.
    /// </summary>
    public byte version;

    /// <summary>
    /// Number of relevant group ids.
    /// </summary>
    public byte groupIdCount;

}

