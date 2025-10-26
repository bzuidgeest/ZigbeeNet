using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Command;

/// <summary>
/// Gets the token data for a single token with provided key
/// Frame value: 0x0102
/// </summary>
public class getTokenData : EzspFrameRequest
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
