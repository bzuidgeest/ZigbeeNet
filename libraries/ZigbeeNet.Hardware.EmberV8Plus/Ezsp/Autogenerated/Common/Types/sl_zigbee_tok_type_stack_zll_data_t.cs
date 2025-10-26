namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// Public API for ZLL stack data token.
/// </summary>
public struct sl_zigbee_tok_type_stack_zll_data_t
{
    /// <summary>
    /// Token bitmask.
    /// </summary>
    public uint bitmask;

    /// <summary>
    /// Minimum free node id.
    /// </summary>
    public ushort freeNodeIdMin;

    /// <summary>
    /// Maximum free node id.
    /// </summary>
    public ushort freeNodeIdMax;

    /// <summary>
    /// Local minimum group id.
    /// </summary>
    public ushort myGroupIdMin;

    /// <summary>
    /// Minimum free group id.
    /// </summary>
    public ushort freeGroupIdMin;

    /// <summary>
    /// Maximum free group id.
    /// </summary>
    public ushort freeGroupIdMax;

    /// <summary>
    /// RSSI correction value.
    /// </summary>
    public byte rssiCorrection;

}

