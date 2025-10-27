using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call gets the radio&apos;s default idle power mode.
/// Frame value: 0x00BA
/// </summary>
public class ZllGetRadioIdleModeResponse : EzspFrameResponse
{
    /// <summary>
    /// The current power mode.
    /// </summary>
    public byte radioIdleMode { get; set; }

}
