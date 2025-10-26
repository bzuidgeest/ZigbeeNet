using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Send Link Power Delta Request from a child to its parent
/// Frame value: 0x00F7
/// </summary>
public class sendLinkPowerDeltaRequestResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of sending the request.
    /// </summary>
    public sl_status_t status { get; set; }

}
