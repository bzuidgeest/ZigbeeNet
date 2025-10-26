using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// This causes to bring down the radio interface other than native.
/// Frame value: 0x00F9
/// </summary>
public class multiPhyStop : EzspFrameRequest
{
    /// <summary>
    /// Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.
    /// </summary>
    public byte phyIndex { get; set; }

}
