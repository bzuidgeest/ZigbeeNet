using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// This retrieves the status of the passed library ID to determine if it is compiled into the stack.
/// Frame value: 0x0001
/// </summary>
public class getLibraryStatus : EzspFrameRequest
{
    /// <summary>
    /// The ID of the library being queried.
    /// </summary>
    public sl_zigbee_library_id_t libraryId { get; set; }

}
