using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Check if a particular counter is one that could report from either a 2.4GHz or sub-GHz interface.
/// Frame value: 0x0132
/// </summary>
public class CounterRequiresPhyIndexResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether this counter requires a PHY index when operating on a dual-PHY system.
    /// </summary>
    public bool requires { get; set; }

}
