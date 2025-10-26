using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sets the power descriptor to the specified value. The power descriptor is a dynamic value. Therefore, you should call this function whenever the value changes.
/// Frame value: 0x0016
/// </summary>
public class setPowerDescriptorResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
