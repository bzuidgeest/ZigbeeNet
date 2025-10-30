using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Sets the radio channel. Calibration occurs if this is the first time the channel has been used.
/// Frame value: 0x008a
/// </summary>
public class MfglibInternalSetChannelRequest : EzspFrameRequest
{
    /// <summary>
    /// The channel to switch to. Valid values are 11 to 26.
    /// </summary>
    public byte channel { get; set; }

