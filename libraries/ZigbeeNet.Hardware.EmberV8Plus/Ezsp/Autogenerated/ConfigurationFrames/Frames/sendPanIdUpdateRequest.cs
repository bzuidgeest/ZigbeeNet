using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Triggers a pan id update message.
/// Frame value: 0x0057
/// </summary>
public class sendPanIdUpdate : EzspFrameRequest
{
    /// <summary>
    /// The new Pan Id
    /// </summary>
    public sl_802154_pan_id_t newPan { get; set; }

}
