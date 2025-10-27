using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x0015
/// </summary>
public class SetManufacturerCodeResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
