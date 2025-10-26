using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Command;

/// <summary>
/// Factory reset all configured zigbee tokens
/// Frame value: 0x0077
/// </summary>
public class tokenFactoryReset : EzspFrameRequest
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
