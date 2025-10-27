using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Gets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x00CA
/// </summary>
public class GetManufacturerCodeResponse : EzspFrameResponse
{
    /// <summary>
    /// The manufacturer code for the local node.
    /// </summary>
    public ushort code { get; set; }

}
