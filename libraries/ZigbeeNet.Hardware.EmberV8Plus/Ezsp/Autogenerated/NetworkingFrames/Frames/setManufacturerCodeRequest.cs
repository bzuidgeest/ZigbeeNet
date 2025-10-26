using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x0015
/// </summary>
public class setManufacturerCode : EzspFrameRequest
{
    /// <summary>
    /// The manufacturer code for the local node.
    /// </summary>
    public ushort code { get; set; }

}
