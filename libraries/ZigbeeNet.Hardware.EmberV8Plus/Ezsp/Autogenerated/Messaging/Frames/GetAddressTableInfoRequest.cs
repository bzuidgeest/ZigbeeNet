using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Gets the EUI64 and short ID of an address table entry.
/// Frame value: 0x005E
/// </summary>
public class GetAddressTableInfoRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of an address table entry.
    /// </summary>
    public byte addressTableIndex { get; set; }

