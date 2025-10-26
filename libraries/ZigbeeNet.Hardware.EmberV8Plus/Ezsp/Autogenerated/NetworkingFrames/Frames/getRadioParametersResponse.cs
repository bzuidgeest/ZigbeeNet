using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Returns the current radio parameters based on phy index.
/// Frame value: 0x00FD
/// </summary>
public class getRadioParametersResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The current radio parameters based on provided phy index.
    /// </summary>
    public sl_zigbee_multi_phy_radio_parameters_t parameters { get; set; }

}
