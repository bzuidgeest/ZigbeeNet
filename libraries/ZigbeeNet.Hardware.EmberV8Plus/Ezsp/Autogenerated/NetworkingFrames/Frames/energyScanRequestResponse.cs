using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Sends a ZDO energy scan request. This request may only be sent by the current network manager and must be unicast, not broadcast. See ezsp-utils.h for related macros sli_zigbee_stack_set_network_manager_request() and sl_zigbee_change_channel_request().
/// Frame value: 0x009C
/// </summary>
public class energyScanRequestResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
