using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Gets the total number of tokens.
/// Frame value: 0x0100
/// </summary>
public class getTokenCountResponse : EzspFrameResponse
{
    /// <summary>
    /// Total number of tokens.
    /// </summary>
    public uint count { get; set; }

}
