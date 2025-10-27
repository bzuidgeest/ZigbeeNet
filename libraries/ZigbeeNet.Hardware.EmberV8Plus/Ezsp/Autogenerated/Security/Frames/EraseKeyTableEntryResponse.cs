using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function erases the data in the key table entry at the specified index. If the index is invalid, false is returned.
/// Frame value: 0x0076
/// </summary>
public class EraseKeyTableEntryResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
