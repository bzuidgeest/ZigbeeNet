using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function erases the data in the key table entry at the specified index. If the index is invalid, false is returned.
/// Frame value: 0x0076
/// </summary>
public class EraseKeyTableEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// This indicates the index of entry to erase.
    /// </summary>
    public byte index { get; set; }

}
