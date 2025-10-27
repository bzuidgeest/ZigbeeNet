using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function searches through the Key Table and tries to find the entry that matches the passed search criteria.
/// Frame value: 0x0075
/// </summary>
public class FindKeyTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// This indicates the index of the entry that matches the search criteria. A value of 0xFF is returned if not matching entry is found.
    /// </summary>
    public byte index { get; set; }

}
