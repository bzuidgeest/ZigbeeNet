using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// A callback invoked by the ZigBee GP stack when a GPDF is received.
/// Frame value: 0x00C5
/// </summary>
public class GpepIncomingMessageHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// GP parameters list represented as a macro for GP endpoint incoming message handler and callbacks prototypes.
    /// </summary>
    public sl_zigbee_gp_params_t param { get; set; }

}
