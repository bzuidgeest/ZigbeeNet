using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Removes the sink table entry stored at the passed index.
/// Frame value: 0x00E0
/// </summary>
public class gpSinkTableRemoveEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested sink table entry.
    /// </summary>
    public byte sinkIndex { get; set; }

}
