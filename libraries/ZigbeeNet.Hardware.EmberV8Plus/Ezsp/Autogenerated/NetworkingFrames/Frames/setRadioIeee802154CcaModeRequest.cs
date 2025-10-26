using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Set the configured 802.15.4 CCA mode in the radio.
/// Frame value: 0x0095
/// </summary>
public class setRadioIeee802154CcaMode : EzspFrameRequest
{
    /// <summary>
    /// A RAIL_IEEE802154_CcaMode_t value.
    /// </summary>
    public byte ccaMode { get; set; }

}
