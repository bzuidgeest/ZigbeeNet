namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

/// <summary>
/// A GP address structure.
/// </summary>
public struct sl_zigbee_gp_address_t
{
    /// <summary>
    /// Contains either a 4-byte source ID or an 8-byte IEEE address, as indicated by the value of the applicationId field.
    /// </summary>
    public fixed byte id[8];

    /// <summary>
    /// The GPD Application ID specifying either source ID (0x00) or IEEE address (0x02).
    /// </summary>
    public byte applicationId;

    /// <summary>
    /// The GPD endpoint.
    /// </summary>
    public byte endpoint;

}

