using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Initialize the radio.  Typically called coming out of deep sleep. For non-sleepy devices, also turns the radio on and leaves it in RX mode.
/// Frame value: 0x0144
/// </summary>
public class StackPowerUpResponse : EzspFrameResponse
{
}
