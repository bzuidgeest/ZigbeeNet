using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Initialize the radio.  Typically called coming out of deep sleep. For non-sleepy devices, also turns the radio on and leaves it in RX mode.
/// Frame value: 0x0144
/// </summary>
public class stackPowerUpResponse : EzspFrameResponse
{
}
