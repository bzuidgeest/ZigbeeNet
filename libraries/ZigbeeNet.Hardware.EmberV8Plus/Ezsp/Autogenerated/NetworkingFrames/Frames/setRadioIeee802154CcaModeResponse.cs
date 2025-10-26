using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Set the configured 802.15.4 CCA mode in the radio.
/// Frame value: 0x0095
/// </summary>
public class setRadioIeee802154CcaModeResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
