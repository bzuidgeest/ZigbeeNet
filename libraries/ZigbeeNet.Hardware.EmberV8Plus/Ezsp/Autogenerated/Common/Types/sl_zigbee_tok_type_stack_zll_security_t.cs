namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Public API for ZLL stack security token.
/// </summary>
public struct sl_zigbee_tok_type_stack_zll_security_t
{
    /// <summary>
    /// Token bitmask.
    /// </summary>
    public uint bitmask;

    /// <summary>
    /// Key index.
    /// </summary>
    public byte keyIndex;

    /// <summary>
    /// Encryption key.
    /// </summary>
    public fixed byte encryptionKey[16];

    /// <summary>
    /// Preconfigured key.
    /// </summary>
    public fixed byte preconfiguredKey[16];

}

