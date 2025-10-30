using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x0015
/// </summary>
public class SetManufacturerCodeRequest : EzspFrameRequest
{
    /// <summary>
    /// The manufacturer code for the local node.
    /// </summary>
    public ushort code { get; set; }

