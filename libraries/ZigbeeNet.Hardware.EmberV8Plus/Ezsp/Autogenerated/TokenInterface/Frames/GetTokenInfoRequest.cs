using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;

/// <summary>
/// Gets the token information for a single token at provided index
/// Frame value: 0x0101
/// </summary>
public class GetTokenInfoRequest : EzspFrameRequest
{
    /// <summary>
    /// Index of the token in the token table for which information is needed.
    /// </summary>
    public byte index { get; set; }

}
