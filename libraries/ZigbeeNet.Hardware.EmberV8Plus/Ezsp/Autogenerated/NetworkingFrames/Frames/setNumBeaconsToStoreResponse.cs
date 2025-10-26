using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Configure the number of beacons to store when issuing active scans for networks.
/// Frame value: 0x0037
/// </summary>
public class setNumBeaconsToStoreResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_INVALID_PARAMETER if numBeacons is greater than SL_ZIGBEE_MAX_BEACONS_TO_STORE, otherwise SL_STATUS_OK
    /// </summary>
    public sl_status_t status { get; set; }

}
