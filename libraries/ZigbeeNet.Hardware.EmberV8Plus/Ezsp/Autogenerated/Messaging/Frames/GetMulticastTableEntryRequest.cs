using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Gets an entry from the multicast table.
/// Frame value: 0x0063
/// </summary>
public class GetMulticastTableEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of a multicast table entry.
    /// </summary>
    public byte index { get; set; }

}
