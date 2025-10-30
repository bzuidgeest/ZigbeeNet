using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// This call is fired when mux detects an invalid rx case, which would be different rx channels for different protocol contexts, when fast cahnnel switching is not enabled
/// Frame value: 0x0062
/// </summary>
public class Mux_invalid_rx_handlerRequest : EzspFrameRequest
{
