using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x0015
/// </summary>
public class setManufacturerCodeResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
