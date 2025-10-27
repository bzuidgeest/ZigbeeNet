namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

/// <summary>
/// GP parameters list.
/// </summary>
public struct sl_zigbee_gp_params_t
{
    /// <summary>
    /// The status of the GPDF receive.
    /// </summary>
    public sl_zigbee_gp_status_t status;

    /// <summary>
    /// The gpdLink value of the received GPDF.
    /// </summary>
    public byte gpdLink;

    /// <summary>
    /// The GPDF sequence number.
    /// </summary>
    public byte sequenceNumber;

    /// <summary>
    /// The address of the source GPD.
    /// </summary>
    public sl_zigbee_gp_address_t addr;

    /// <summary>
    /// The security level of the received GPDF.
    /// </summary>
    public sl_zigbee_gp_security_level_t gpdfSecurityLevel;

    /// <summary>
    /// The securityKeyType used to decrypt/authenticate the incoming GPDF.
    /// </summary>
    public sl_zigbee_gp_key_type_t gpdfSecurityKeyType;

    /// <summary>
    /// Whether the incoming GPDF had the auto-commissioning bit set.
    /// </summary>
    public bool autoCommissioning;

    /// <summary>
    /// Bidirectional information represented in bitfields, where bit0 holds the rxAfterTx of incoming GPDF and bit1 holds if TX queue is available for outgoing GPDF.
    /// </summary>
    public byte bidirectionalInfo;

    /// <summary>
    /// The security frame counter of the incoming GPDF.
    /// </summary>
    public uint gpdSecurityFrameCounter;

    /// <summary>
    /// The gpdCommandId of the incoming GPDF.
    /// </summary>
    public byte gpdCommandId;

    /// <summary>
    /// The received MIC of the GPDF.
    /// </summary>
    public uint mic;

    /// <summary>
    /// The proxy table index of the corresponding proxy table entry to the incoming GPDF.
    /// </summary>
    public byte proxyTableIndex;

    /// <summary>
    /// The length of the GPD command payload.
    /// </summary>
    public byte gpdCommandPayloadLength;

    /// <summary>
    /// The GPD command payload.
    /// </summary>
    // Array field with symbolic size: SL_ZIGBEE_GP_MAX_APPLICATION_PAYLOAD
    // public fixed byte gpdCommandPayload[SL_ZIGBEE_GP_MAX_APPLICATION_PAYLOAD];

    /// <summary>
    /// Rx packet information.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo;

}

