using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// This retrieves the status of the passed library ID to determine if it is compiled into the stack.
/// Frame value: 0x0001
/// </summary>
public class GetLibraryStatusRequest : EzspFrameRequest
{
    /// <summary>
    /// The ID of the library being queried.
    /// </summary>
    public sl_zigbee_library_id_t libraryId { get; set; }

}
