using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call sets the radio&apos;s default idle power mode.
/// Frame value: 0x00D4
/// </summary>
public class ZllSetRadioIdleModeRequest : EzspFrameRequest
{
    /// <summary>
    /// The power mode to be set.
    /// </summary>
    public sl_zigbee_radio_power_mode_t mode { get; set; }

