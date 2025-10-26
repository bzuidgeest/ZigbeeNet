namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Describes the initial security features and requirements that will be used when forming or joining ZLL networks.
/// </summary>
public struct sl_zigbee_zll_initial_security_state_t
{
    /// <summary>
    /// Unused bitmask; reserved for future use.
    /// </summary>
    public uint bitmask;

    /// <summary>
    /// The key encryption algorithm advertised by the application.
    /// </summary>
    public sl_zigbee_zll_key_index_t keyIndex;

    /// <summary>
    /// The encryption key for use by algorithms that require it.
    /// </summary>
    public sl_zigbee_key_data_t encryptionKey;

    /// <summary>
    /// The pre-configured link key used during classical ZigBee commissioning.
    /// </summary>
    public sl_zigbee_key_data_t preconfiguredKey;

}

