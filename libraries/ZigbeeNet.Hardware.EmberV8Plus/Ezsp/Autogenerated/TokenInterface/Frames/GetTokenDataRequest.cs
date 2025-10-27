using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;

/// <summary>
/// Gets the token data for a single token with provided key
/// Frame value: 0x0102
/// </summary>
public class GetTokenDataRequest : EzspFrameRequest
{
    /// <summary>
    /// Key of the token in the token table for which data is needed.
    /// </summary>
    public uint token { get; set; }

    /// <summary>
    /// Index in case of the indexed token.
    /// </summary>
    public uint index { get; set; }

}
