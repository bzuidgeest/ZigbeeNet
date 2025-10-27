using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Frames;

/// <summary>
/// Factory reset all configured zigbee tokens
/// Frame value: 0x0077
/// </summary>
public class TokenFactoryResetRequest : EzspFrameRequest
{
    /// <summary>
    /// Exclude network and APS outgoing frame counter tokens.
    /// </summary>
    public bool excludeOutgoingFC { get; set; }

    /// <summary>
    /// Exclude stack boot counter token.
    /// </summary>
    public bool excludeBootCounter { get; set; }

}
