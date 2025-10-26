using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Check if a particular counter is one that could report from either a 2.4GHz or sub-GHz interface.
/// Frame value: 0x0132
/// </summary>
public class counterRequiresPhyIndexResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether this counter requires a PHY index when operating on a dual-PHY system.
    /// </summary>
    public bool requires { get; set; }

}
