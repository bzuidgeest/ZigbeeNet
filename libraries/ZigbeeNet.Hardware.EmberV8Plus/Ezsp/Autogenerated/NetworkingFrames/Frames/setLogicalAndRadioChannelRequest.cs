using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// This call sets the radio channel in the stack and propagates the information to the hardware.
/// Frame value: 0x00B9
/// </summary>
public class setLogicalAndRadioChannel : EzspFrameRequest
{
    /// <summary>
    /// The radio channel to be set.
    /// </summary>
    public byte radioChannel { get; set; }

}
