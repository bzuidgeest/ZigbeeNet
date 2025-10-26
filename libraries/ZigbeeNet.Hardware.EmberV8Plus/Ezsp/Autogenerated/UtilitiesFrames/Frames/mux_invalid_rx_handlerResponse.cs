using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// This call is fired when mux detects an invalid rx case, which would be different rx channels for different protocol contexts, when fast cahnnel switching is not enabled
/// Frame value: 0x0062
/// </summary>
public class mux_invalid_rx_handlerResponse : EzspFrameResponse
{
    public byte new_rx_channel { get; set; }

    public byte old_rx_channel { get; set; }

}
