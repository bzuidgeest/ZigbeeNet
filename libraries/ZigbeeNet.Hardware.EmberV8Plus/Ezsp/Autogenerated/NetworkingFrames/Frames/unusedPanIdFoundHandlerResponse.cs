using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// This function returns an unused panID and channel pair found via the find unused panId scan procedure.
/// Frame value: 0x00D2
/// </summary>
public class unusedPanIdFoundHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The unused panID which has been found.
    /// </summary>
    public sl_802154_pan_id_t panId { get; set; }

    /// <summary>
    /// The channel that the unused panID was found on.
    /// </summary>
    public byte channel { get; set; }

}
