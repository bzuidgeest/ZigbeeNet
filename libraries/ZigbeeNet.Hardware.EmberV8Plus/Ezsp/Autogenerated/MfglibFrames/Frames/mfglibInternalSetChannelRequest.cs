using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Command;

/// <summary>
/// Sets the radio channel. Calibration occurs if this is the first time the channel has been used.
/// Frame value: 0x008a
/// </summary>
public class mfglibInternalSetChannel : EzspFrameRequest
{
    /// <summary>
    /// The channel to switch to. Valid values are 11 to 26.
    /// </summary>
    public byte channel { get; set; }

}
