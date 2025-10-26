namespace ZigBeeNet.EmberV8Plus.TokenInterfaceFrames.Types;

/// <summary>
/// Information of a token in the token table.
/// </summary>
public struct sl_zigbee_token_info_t
{
    /// <summary>
    /// NVM3 key of the token
    /// </summary>
    public uint nvm3Key;

    /// <summary>
    /// Token is a counter type
    /// </summary>
    public bool isCnt;

    /// <summary>
    /// Token is an indexed token
    /// </summary>
    public bool isIdx;

    /// <summary>
    /// Size of the token
    /// </summary>
    public byte size;

    /// <summary>
    /// Array size of the token
    /// </summary>
    public byte arraySize;

}

