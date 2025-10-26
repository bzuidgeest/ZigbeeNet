using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Command;

/// <summary>
/// Gets the token information for a single token at provided index
/// Frame value: 0x0101
/// </summary>
public class getTokenInfo : EzspFrameRequest
{
    /// <summary>
    /// Index of the token in the token table for which information is needed.
    /// </summary>
    public byte index { get; set; }

}
