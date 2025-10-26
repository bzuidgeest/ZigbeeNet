using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Gets the priority masks and related variables for choosing the best beacon.
/// Frame value: 0x00F3
/// </summary>
public class getBeaconClassificationParamsResponse : EzspFrameResponse
{
    /// <summary>
    /// The attempt to get the pramaters returns SL_STATUS_OK
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Gets the beacon prioritization related variable
    /// </summary>
    public sl_zigbee_beacon_classification_params_t param { get; set; }

}
