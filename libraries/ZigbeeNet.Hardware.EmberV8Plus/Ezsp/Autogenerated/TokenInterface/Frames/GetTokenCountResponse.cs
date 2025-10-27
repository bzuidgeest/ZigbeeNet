using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;

/// <summary>
/// Gets the total number of tokens.
/// Frame value: 0x0100
/// </summary>
public class GetTokenCountResponse : EzspFrameResponse
{
    /// <summary>
    /// Total number of tokens.
    /// </summary>
    public uint count { get; set; }

}
