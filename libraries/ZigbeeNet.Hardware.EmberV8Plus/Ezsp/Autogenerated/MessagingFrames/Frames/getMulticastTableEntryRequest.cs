using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Gets an entry from the multicast table.
/// Frame value: 0x0063
/// </summary>
public class getMulticastTableEntry : EzspFrameRequest
{
    /// <summary>
    /// The index of a multicast table entry.
    /// </summary>
    public byte index { get; set; }

}
