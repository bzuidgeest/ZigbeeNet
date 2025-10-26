using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Returns the current radio parameters based on phy index.
/// Frame value: 0x00FD
/// </summary>
public class getRadioParameters : EzspFrameRequest
{
    /// <summary>
    /// Desired index of phy interface for radio parameters.
    /// </summary>
    public byte phyIndex { get; set; }

}
