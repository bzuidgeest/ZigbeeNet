using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Set the radio power value for a given child index.
/// Frame value: 0x0135
/// </summary>
public class setChildPower : EzspFrameRequest
{
    /// <summary>
    /// The index.
    /// </summary>
    public byte childIndex { get; set; }

    /// <summary>
    /// The new power value.
    /// </summary>
    public sbyte newPower { get; set; }

}
