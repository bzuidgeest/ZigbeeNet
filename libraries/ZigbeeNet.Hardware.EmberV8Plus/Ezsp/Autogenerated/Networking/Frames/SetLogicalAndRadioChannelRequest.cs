using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This call sets the radio channel in the stack and propagates the information to the hardware.
/// Frame value: 0x00B9
/// </summary>
public class SetLogicalAndRadioChannelRequest : EzspFrameRequest
{
    /// <summary>
    /// The radio channel to be set.
    /// </summary>
    public byte radioChannel { get; set; }

}
