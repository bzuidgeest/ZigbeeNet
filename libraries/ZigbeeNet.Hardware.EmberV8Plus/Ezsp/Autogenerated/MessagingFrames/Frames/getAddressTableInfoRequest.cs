using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Gets the EUI64 and short ID of an address table entry.
/// Frame value: 0x005E
/// </summary>
public class getAddressTableInfo : EzspFrameRequest
{
    /// <summary>
    /// The index of an address table entry.
    /// </summary>
    public byte addressTableIndex { get; set; }

}
