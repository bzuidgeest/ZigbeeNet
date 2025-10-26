namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Types;

/// <summary>
/// Token Data
/// </summary>
public struct sl_zigbee_token_data_t
{
    /// <summary>
    /// Token data size in bytes
    /// </summary>
    public uint size;

    /// <summary>
    /// Token data pointer
    /// </summary>
    public fixed byte data[64];

}

